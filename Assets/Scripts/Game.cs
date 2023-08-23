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
    private List<String> letters = new List<String> { "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�" };
    //{ "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�" };
    private int lc;
    public string letter;
    [SerializeField] private GameObject correct;
    [SerializeField] private GameObject uncorrect;
    private List<String> lettersFind = new List<String>();
    void Start()
    {
        lc = letters.Count;
        letter = letters[Random.Range(0, letters.Count)];
        text.text = "����� ����� " + letter;
        letters.Remove(letter);
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
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("wait for 3 sec");
    }
    void NewAppropriation()
    {
        lettersFind.Add(letter);
        Debug.Log(lettersFind.Count);
        StartCoroutine(Waiter());
        if (lettersFind.Count == lc)
        {
            Win();
            Debug.Log("win");
        } else {
            string newLetter = letters[Random.Range(0, letters.Count)];
            letter = newLetter;
            letters.Remove(letter);
            text.text = "����� ����� " + letter;
        }
    }
    void Win()
    {
        text.text = "�� ����� ��� �����!";
        Invoke("Exit", 3);
    }
    private void Exit()
    {
        Debug.Log("Exit in menu");
        SceneManager.LoadScene("Menu");
    }
}
