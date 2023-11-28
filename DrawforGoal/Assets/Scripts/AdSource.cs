using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdSource : MonoBehaviour
{
    public enum AdProviders
    {
        UnityAds, AdMob, TestProvider
    }
    public AdProviders CurrentProvider;
    public static AdSource Instance { get; private set; }

    IAdprovider currentProvider = null;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        CheckProvider();
    }
    private void CheckProvider()
    {
        switch (CurrentProvider)
        {
            case AdProviders.UnityAds:
                break;

            case AdProviders.AdMob:
                break;
            case AdProviders.TestProvider:
                currentProvider = new TestAdProvider();
                break;
        }
    }
    public IAdprovider GetAdProvider()
    {
        return currentProvider;
    }
}
