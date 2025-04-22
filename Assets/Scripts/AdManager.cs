using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    #region Singleton
    public static AdManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }
    #endregion

    private readonly string testInterstitialUnitID = "ca-app-pub-3940256099942544/1033173712";
    private readonly string testRewardedUnitID = "ca-app-pub-3940256099942544/5354046379";

    private InterstitialAd onDeathInterstitialAd;
    private RewardedAd resumeVideoAd;

    private void Start()
    {
        LoadNewInterstitialAd(null, null);
        LoadNewVideoAd(null, null);
    }

    #region Interstitial
    public void ShowDeathInterstitial()
    {
        if (onDeathInterstitialAd.IsLoaded())
            onDeathInterstitialAd.Show();
    }

    private void LoadNewInterstitialAd(object sender, EventArgs args)
    {
        onDeathInterstitialAd = new InterstitialAd(testInterstitialUnitID);
        onDeathInterstitialAd.LoadAd(new AdRequest.Builder().Build());
        onDeathInterstitialAd.OnAdClosed += LoadNewInterstitialAd;
    }
    #endregion

    #region RewardedAd
    public void WatchResumeAd()
    {
        if (resumeVideoAd.IsLoaded())
            resumeVideoAd.Show();
    }

    private void LoadNewVideoAd(object sender, EventArgs args)
    {
        resumeVideoAd = new RewardedAd(testRewardedUnitID);
        resumeVideoAd.LoadAd(new AdRequest.Builder().Build());
        resumeVideoAd.OnAdClosed += LoadNewVideoAd;
        resumeVideoAd.OnUserEarnedReward += RewardResume;
    }

    private void RewardResume(object sender, Reward reward)
    {
        GameManager.instance.Resume();
    }
    #endregion
}
