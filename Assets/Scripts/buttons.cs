using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttons : MonoBehaviour
{
    [SerializeField]private Button buttonExitInMain;
    public void RepeatLetters()
    {
        SceneManager.LoadScene("Letters");
        buttonExitInMain.onClick.AddListener(() => ExitInMain());
    }
    public void Game()
    {
        SceneManager.LoadScene("Game");
        buttonExitInMain.onClick.AddListener(() => ExitInMain());
    }
    public void ExitInMain()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
