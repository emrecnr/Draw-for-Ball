using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdprovider
{
    bool IsRewardedAdReady { get; }
    void InitializeAd();
    void ShowRewarded(Action onRewarded, Action onQuitted);
    void ShowIntersitialAd();
}


