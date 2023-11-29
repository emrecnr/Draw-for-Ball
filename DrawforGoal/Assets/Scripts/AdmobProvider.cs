using GoogleMobileAds.Api;
using System;
using UnityEngine;


public class AdmobProvider : IAdprovider
{

    //private const string rewardedAdID = "ca-app-pub-3940256099942544/5224354917";
    private string _adUnitId = "ca-app-pub-6788314039807028/3817511417";
    private RewardedAd _rewardedAd;
    private bool _rewardedAdReady;
    public bool IsRewardedAdReady => _rewardedAdReady;
    public AdmobProvider()
    {
        InitializeAd();
    }
    public void InitializeAd()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            
        });
        LoadAd();
    }
    public void LoadAd()
    {
       
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        
          var adRequest = new AdRequest();

        
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    _rewardedAdReady = false;
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());
                _rewardedAdReady = true;
                _rewardedAd = ad;
                RegisterEventHandlers(_rewardedAd);
            });

    }


    public void ShowRewarded(Action onRewarded, Action onQuitted)
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                onRewarded?.Invoke();
            });
        }
    }


    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadAd();
        };
    }
}
