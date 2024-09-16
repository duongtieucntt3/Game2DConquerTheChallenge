using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public BannerAds bannerAds;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;

    public static AdsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
       // DontDestroyOnLoad(gameObject);

        bannerAds.LoadBannerAd();
        interstitialAds.LoadInterstitalAd();
        rewardedAds.LoadRewardedAd();
    }


}
