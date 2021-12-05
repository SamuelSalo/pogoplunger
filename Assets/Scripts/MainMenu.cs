using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using GooglePlayGames;
using UnityEngine.UI;
using TMPro;
using GooglePlayGames.BasicApi;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown controlsDropdown;
    public TMP_Text authButtonText;

    private void Awake()
    {
        controlsDropdown.value = PlayerPrefs.GetInt("PogoPlunger_MotionControls", 0);
    }
    private void Update()
    {
        authButtonText.text = PlayGamesPlatform.Instance.IsAuthenticated() ? "SIGN OUT" : "SIGN IN";
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetControls(int value)
    {
        PlayerPrefs.SetInt("PogoPlunger_MotionControls", value);
    }

    public void OpenLeaderboard()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI9syNqfAYEAIQAA");
        }
    }

    public void Auth()
    {
        if(PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayServices.SignOut();
        }
        else
        {
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) => {
                SignInStatus status = result;

                string errMsg = result switch
                {
                    SignInStatus.Success => "Sign in succeeded",
                    SignInStatus.UiSignInRequired => "Ui sign in required",
                    SignInStatus.DeveloperError => "Dev Error! Application is misconfigured.",
                    SignInStatus.NetworkError => "Networking error",
                    SignInStatus.InternalError => "Internal error",
                    SignInStatus.Canceled => "Sign in was canceled",
                    SignInStatus.AlreadyInProgress => "Sign in already in progress",
                    SignInStatus.Failed => "Sign in failed with unknown reason. Check adb log!",
                    SignInStatus.NotAuthenticated => "Not authenticated! Sign in!",
                    _ => throw new System.NotImplementedException(),
                };

                Debug.Log(errMsg);
            });
        }
    }
}