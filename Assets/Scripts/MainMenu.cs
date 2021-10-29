using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
  
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void OpenLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI9syNqfAYEAIQAA");
    }

    public void SignOut()
    {
        PlayServices.SignOut();
    }
}
