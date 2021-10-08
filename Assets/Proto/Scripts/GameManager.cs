using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    private int currentScore = 0;
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
        currentScore++;
        scoreText.text = currentScore.ToString();
    }

    public void Death()
    {
        if(PlayerPrefs.HasKey("PogoPlunger_Highscore"))
        {
            if (PlayerPrefs.GetInt("PogoPlunger_Highscore") < currentScore)
            {
                PlayerPrefs.SetInt("PogoPlunger_Highscore", currentScore);
                PlayServices.PostLeaderboardScore(currentScore);
                //todo new highscore fx
            }
        }
        else
        {
            PlayerPrefs.SetInt("PogoPlunger_Highscore", currentScore);
            PlayServices.PostLeaderboardScore(currentScore);
            //todo new highscore fx
        }

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
