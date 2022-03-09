using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Events;

public class AdmobManager : MonoBehaviour
{
    [Header("Banner Settings")]
    [SerializeField] private string bannerAdUnitIdAndroid = "ca-app-pub-3940256099942544/6300978111";
    [SerializeField] private string bannerAdUnitIdIOS = "ca-app-pub-3940256099942544/2934735716";
    [SerializeField] private List<Banner> banners;
    private BannerView bannerView;


    [Header("Interstitial Settings")]
    [SerializeField] private string interstitialAdUnitIdAndroid = "ca-app-pub-3940256099942544/1033173712";
    [SerializeField] private string interstitialAdUnitIdIOS = "ca-app-pub-3940256099942544/4411468910";
    [SerializeField] private UnityEvent OnShowInterstitial;
    [SerializeField] private UnityEvent OnCloseInterstitial;
    private InterstitialAd interstitial;


    private void Awake()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        RequestInterstitial();
    }



    #region Banner


    private void RequestBanner()
    {
    #if UNITY_ANDROID
            string adUnitId = bannerAdUnitIdAndroid;
#elif UNITY_IPHONE
                string adUnitId = bannerAdUnitIdIOS;
#else
                string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Leaderboard, AdPosition.Bottom);
    }

    public void ShowBannerWithDelay(float delay)
    {
        StartCoroutine(WaitToShowBanner(delay));
    }
    private IEnumerator WaitToShowBanner(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowBanner();
    }
    public void ShowBanner()
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
    }
    public void CloseBanner()
    {
        bannerView.Destroy();
    }



    #endregion



    #region Interstitial


    private void RequestInterstitial()
    {
    #if UNITY_ANDROID
            string adUnitId = interstitialAdUnitIdAndroid;
    #elif UNITY_IPHONE
                string adUnitId = interstitialAdUnitIdIOS;
    #else
                string adUnitId = "unexpected_platform";
    #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    public void ShowInterstitialWithDelay(float delay)
    {
        StartCoroutine(WaitToShowInterstitial(delay));
    }
    private IEnumerator WaitToShowInterstitial(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowInterstitial();
    }
    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
    public void CloseInterstitial()
    {
        interstitial.Destroy();
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        OnShowInterstitial.Invoke();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        OnCloseInterstitial.Invoke();
    }




    #endregion




    private void OnApplicationQuit()
    {
        CloseBanner();
    }
}
[Serializable]
public class Banner
{
    public AdPosition adPosition;
}

