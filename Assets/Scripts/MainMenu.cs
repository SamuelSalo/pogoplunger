using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using GooglePlayGames;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Toggle motionControlsToggle;

    private void Awake()
    {
        motionControlsToggle.isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt("PogoPlunger_MotionControls", 0));
    }
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

    public void ToggleMotionControls(bool value)
    {
        PlayerPrefs.SetInt("PogoPlunger_MotionControls", System.Convert.ToInt32(value));
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
