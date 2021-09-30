using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        
    }
}
