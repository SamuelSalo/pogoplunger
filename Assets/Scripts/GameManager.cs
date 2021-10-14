using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }
    #endregion

    public int CurrentScore { get; private set; }
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public GameObject deathMenu;
    public GameObject gameUI;
    public Rigidbody2D player;

    private void Start()
    {
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("PogoPlunger_Highscore");
    }
    public void AddScore()
    {
        CurrentScore++;
        scoreText.text = CurrentScore.ToString();
    }

    public void Death()
    {
        deathMenu.SetActive(true);
        gameUI.SetActive(false);
        player.constraints = RigidbodyConstraints2D.FreezeAll;
        AdManager.instance.ShowDeathInterstitial();
    }

    public void Resume()
    {
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
        deathMenu.SetActive(false);
        gameUI.SetActive(true);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        ConfirmDeath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ConfirmDeath()
    {
        if (PlayerPrefs.HasKey("PogoPlunger_Highscore"))
        {
            if (PlayerPrefs.GetInt("PogoPlunger_Highscore") < CurrentScore)
            {
                PlayerPrefs.SetInt("PogoPlunger_Highscore", CurrentScore);
                PlayServices.PostLeaderboardScore(CurrentScore);
                //todo new highscore fx
            }
        }
        else
        {
            PlayerPrefs.SetInt("PogoPlunger_Highscore", CurrentScore);
            PlayServices.PostLeaderboardScore(CurrentScore);
            //todo new highscore fx
        }

        AnalyticsResult result = Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
            { "Points", CurrentScore}
        });

        Debug.Log("Analytics Result: " + result);
    }
}
