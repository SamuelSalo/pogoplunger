using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

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
    }

    public static void PostLeaderboardScore(int _score)
    {
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
    }
}
