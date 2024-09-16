
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
        bannerAds.LoadBannerAd();
        interstitialAds.LoadInterstitalAd();
        rewardedAds.LoadRewardedAd();
    }


}
