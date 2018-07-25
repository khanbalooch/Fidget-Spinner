using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
public class AdmobIntegration : MonoBehaviour
{
    void Awake()
    {
        
    }
    
    void Start ()
    {
        this.RequestBanner();
        Debug.Log("Ad Loading");
        
    }

    private void RequestBanner()
    {
        #if UNITY_EDITOR
                string adUnitId = "ca-app-pub-3840620948536981/1651801756";
        #elif UNITY_ANDROID
                string adUnitId = "ca-app-pub-3840620948536981/1651801756";
        #elif UNITY_IPHONE
                string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
        #else
                string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-3840620948536981/4605268153";
        #elif UNITY_IPHONE
                string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
        #else
                string adUnitId = "ca-app-pub-3840620948536981/4605268153";
        #endif

        // Initialize an InterstitialAd.
        InterstitialAd interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }
}
