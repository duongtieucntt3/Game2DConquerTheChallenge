using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBannerAds : MonoBehaviour
{
    private void Awake()
    {
        AdsManager.Instance.bannerAds.ShowBannerAd();
    }

}
