using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GoogleMobileAds.Api;
using System;

public class PlayServices
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Login()
    {
        
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
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
        });

        MobileAds.Initialize(initStatus => { });
    }

    public static void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    public static void PostLeaderboardScore(int _score)
    {
#if !UNITY_EDITOR
        Social.ReportScore(_score, "CgkI9syNqfAYEAIQAA", (bool success) => {
            
            if(success)
            {
                Debug.Log("Posted score to leaderboard: " + _score);
            }
            else
            {
                Debug.LogWarning("Failed to post score to leaderboard: " + _score);
            }
        });
#endif
    }
}
