using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class Game : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private AudioClip[] arrayClip;
    private AudioSource audioSource;
    private List<String> letters = new List<String> { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
    //{ "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
    private int lc;
    private string txt;
    public string letter;
    [SerializeField] private GameObject correct;
    [SerializeField] private GameObject uncorrect;
    private List<String> lettersFind = new List<String>();
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lc = letters.Count;
        int rnd = Random.Range(0, letters.Count);
        letter = letters[rnd];
        txt = "Найди букву " + letter;
        text.text = txt;
        audioSource.clip = arrayClip[rnd];
        audioSource.Play();
        letters.Remove(letter);
        RemoveElement(ref arrayClip, rnd);
    }

    public void Right()
    {
        correct.gameObject.SetActive(true);
        StartCoroutine(waiterRight());
        NewAppropriation();
    }
    public void Wrong()
    {
        uncorrect.gameObject.SetActive(true);
        StartCoroutine(waiterWrong());
    }
    IEnumerator waiterRight()
    {
        yield return new WaitForSeconds(2);
        correct.gameObject.SetActive(false);
    }
    IEnumerator waiterWrong()
    {
        yield return new WaitForSeconds(2);
        uncorrect.gameObject.SetActive(false);
    }

    IEnumerator WaiterClip(AudioClip clip)
    {
        float counter = 0;

        //Wait for 5 seconds
        float waitTime = 5;
        while (counter < waitTime)
        {
            counter += Time.deltaTime;
            Debug.Log("We have waited for: " + counter + " seconds");
            yield return null;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
    IEnumerator WaiterLetter(string txt)
    {
        float counter = 0;

        //Wait for 3 seconds
        float waitTime = 3;
        while (counter < waitTime)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        text.text = txt;
    }
    void RemoveElement <T>(ref T[] arr, int index)
    {
        for(int i= index; i < arr.Length-1; i++)
        {
            arr[i] = arr[i+1];
        }
        Array.Resize(ref arr, arr.Length-1);
    }
    void NewAppropriation()
    {
        lettersFind.Add(letter);
        Debug.Log(lettersFind.Count);
        if (lettersFind.Count == lc)
        {
            Win();
            Debug.Log("win");
        } else {
            int rnd = Random.Range(0, letters.Count);
            Debug.Log(rnd);
            string newLetter = letters[rnd];
            AudioClip newClip = arrayClip[rnd];
            letter = newLetter;
            letters.Remove(letter);
            RemoveElement(ref arrayClip, rnd);
            txt = "Найди букву " + letter;
            StartCoroutine(WaiterLetter(txt));
            StartCoroutine(WaiterClip(newClip));
        }
    }
    void Win()
    {
        text.text = "Вы нашли все буквы!";
        Invoke("Exit", 3);
    }
    private void Exit()
    {
        Debug.Log("Exit in menu");
        SceneManager.LoadScene("Menu");
    }
}
