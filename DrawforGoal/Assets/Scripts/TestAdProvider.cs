using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAdProvider : IAdprovider
{
    private bool _isRewardedAdReady = false;
    public bool IsRewardedAdReady => _isRewardedAdReady;

    public TestAdProvider()
    {
        InitializeAd();
    }
    public void InitializeAd()
    {
        //Initialize Ad 
        Debug.Log("Test Ad Initialize");
    }

    public void ShowRewarded(Action onRewarded, Action onQuitted)
    {
        // Show Rewarded Ad
        onRewarded?.Invoke();
        Debug.Log("Test Reklam� G�sterildi");
    }

    public void ShowIntersitialAd()
    {
       // Show Intersitial Ad
    }
}
