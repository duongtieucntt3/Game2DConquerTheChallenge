using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBannerAds : MonoBehaviour
{
    private void Awake()
    {
        this.Show();
    }

    private async void Show()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        AdsManager.Instance.bannerAds.ShowBannerAd();
    }
}
