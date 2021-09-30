using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("pausemenutest");
        Time.timeScale = 1f;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
