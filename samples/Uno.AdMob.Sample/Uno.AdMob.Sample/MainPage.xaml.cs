using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using Uno.AdMob.Services;

namespace Uno.AdMob.Sample;

public sealed partial class MainPage : Page
{
    private readonly ObservableCollection<string> _log = new();

    public MainPage()
    {
        this.InitializeComponent();

        LogList.ItemsSource = _log;
        SupportedText.Text = $"AdMobPlatform.IsSupported = {AdMobPlatform.IsSupported}";

        Banner.OnAdLoaded += (s, e) => Log("Banner: OnAdLoaded");
        Banner.OnAdFailedToLoad += (s, e) => Log($"Banner: OnAdFailedToLoad: {e.Message}");
        Banner.OnAdImpression += (s, e) => Log("Banner: OnAdImpression");
        Banner.OnAdClicked += (s, e) => Log("Banner: OnAdClicked");
        Banner.OnAdOpened += (s, e) => Log("Banner: OnAdOpened");
        Banner.OnAdClosed += (s, e) => Log("Banner: OnAdClosed");

        if (AdMobPlatform.IsSupported)
        {
            var consent = GetService<IAdConsentService>();
            consent.OnConsentInfoUpdated += (s, info) =>
            {
                Log($"Consent: info updated, CanRequestAds={info?.CanRequestAds}");
                RefreshPrivacyOptions();
            };
            consent.OnConsentInfoFailedToUpdate += (s, e) => Log($"Consent: info failed to update: {e.Message}");
            consent.OnConsentFormDismissed += (s, e) =>
            {
                Log("Consent: form dismissed");
                RefreshPrivacyOptions();
            };
            consent.OnConsentFormError += (s, e) => Log($"Consent: form error: {e.Message}");
            RefreshPrivacyOptions();
        }
    }

    private static T GetService<T>()
        where T : notnull => AdMobPlatform.Services!.GetRequiredService<T>();

    private void Log(string text) => DispatcherQueue.TryEnqueue(() => _log.Insert(0, $"{DateTime.Now:HH:mm:ss} {text}"));

    private void RefreshPrivacyOptions() => DispatcherQueue.TryEnqueue(() =>
        PrivacyOptionsButton.IsEnabled = AdMobPlatform.IsSupported && GetService<IAdConsentService>().IsPrivacyOptionsRequired());

    private void OnInterstitialClick(object sender, RoutedEventArgs e)
    {
        var ad = GetService<IInterstitialAdService>().CreateAd();
        ad.OnAdLoaded += (s, a) =>
        {
            Log("Interstitial: OnAdLoaded");
            ad.Show();
        };
        ad.OnAdFailedToLoad += (s, error) => Log($"Interstitial: OnAdFailedToLoad: {error.Message}");
        ad.OnAdShowed += (s, a) => Log("Interstitial: OnAdShowed");
        ad.OnAdFailedToShow += (s, error) => Log($"Interstitial: OnAdFailedToShow: {error.Message}");
        ad.OnAdImpression += (s, a) => Log("Interstitial: OnAdImpression");
        ad.OnAdClicked += (s, a) => Log("Interstitial: OnAdClicked");
        ad.OnAdDismissed += (s, a) => Log("Interstitial: OnAdDismissed");
        ad.Load();
    }

    private void OnRewardedClick(object sender, RoutedEventArgs e)
    {
        var ad = GetService<IRewardedAdService>().CreateAd();
        ad.OnAdLoaded += (s, a) =>
        {
            Log("Rewarded: OnAdLoaded");
            ad.Show();
        };
        ad.OnAdFailedToLoad += (s, error) => Log($"Rewarded: OnAdFailedToLoad: {error.Message}");
        ad.OnAdShowed += (s, a) => Log("Rewarded: OnAdShowed");
        ad.OnAdFailedToShow += (s, error) => Log($"Rewarded: OnAdFailedToShow: {error.Message}");
        ad.OnAdImpression += (s, a) => Log("Rewarded: OnAdImpression");
        ad.OnAdClicked += (s, a) => Log("Rewarded: OnAdClicked");
        ad.OnAdDismissed += (s, a) => Log("Rewarded: OnAdDismissed");
        ad.OnUserEarnedReward += (s, reward) => Log($"Rewarded: OnUserEarnedReward {reward.Amount} {reward.Type}");
        ad.Load();
    }

    private void OnRewardedInterstitialClick(object sender, RoutedEventArgs e)
    {
        var ad = GetService<IRewardedInterstitialAdService>().CreateAd();
        ad.OnAdLoaded += (s, a) =>
        {
            Log("Rewarded interstitial: OnAdLoaded");
            ad.Show();
        };
        ad.OnAdFailedToLoad += (s, error) => Log($"Rewarded interstitial: OnAdFailedToLoad: {error.Message}");
        ad.OnAdShowed += (s, a) => Log("Rewarded interstitial: OnAdShowed");
        ad.OnAdFailedToShow += (s, error) => Log($"Rewarded interstitial: OnAdFailedToShow: {error.Message}");
        ad.OnAdImpression += (s, a) => Log("Rewarded interstitial: OnAdImpression");
        ad.OnAdClicked += (s, a) => Log("Rewarded interstitial: OnAdClicked");
        ad.OnAdDismissed += (s, a) => Log("Rewarded interstitial: OnAdDismissed");
        ad.OnUserEarnedReward += (s, reward) => Log($"Rewarded interstitial: OnUserEarnedReward {reward.Amount} {reward.Type}");
        ad.Load();
    }

    private void OnAppOpenClick(object sender, RoutedEventArgs e)
    {
        var ad = GetService<IAppOpenAdService>().CreateAd();
        ad.OnAdLoaded += (s, a) =>
        {
            Log("App open: OnAdLoaded");
            ad.Show();
        };
        ad.OnAdFailedToLoad += (s, error) => Log($"App open: OnAdFailedToLoad: {error.Message}");
        ad.OnAdShowed += (s, a) => Log("App open: OnAdShowed");
        ad.OnAdFailedToShow += (s, error) => Log($"App open: OnAdFailedToShow: {error.Message}");
        ad.OnAdImpression += (s, a) => Log("App open: OnAdImpression");
        ad.OnAdClicked += (s, a) => Log("App open: OnAdClicked");
        ad.OnAdDismissed += (s, a) => Log("App open: OnAdDismissed");
        ad.Load();
    }

    private void OnPrivacyOptionsClick(object sender, RoutedEventArgs e) => GetService<IAdConsentService>().ShowPrivacyOptionsForm();

    private void OnResetConsentClick(object sender, RoutedEventArgs e)
    {
        GetService<IAdConsentService>().Reset();
        Log("Consent: reset. Restart the app to see the form again.");
        RefreshPrivacyOptions();
    }
}
