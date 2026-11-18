using Microsoft.Extensions.DependencyInjection; // Uno: replaces Microsoft.Maui.LifecycleEvents
using Uno.AdMob.Configuration;
using Uno.AdMob.Services;

namespace Uno.AdMob;

/// <summary>
/// Handles plugin initialization.
/// </summary>
public static class Config
{
    /// <summary>
    /// Registers the AdMob plugin's services and view handlers.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="androidDefaultBannerAdUnitId">Optional banner ad unit ID to be used by default on Android. See <see cref="AdConfig.DefaultBannerAdUnitId" />.</param>
    /// <param name="androidDefaultInterstitialAdUnitId">Optional interstitial ad unit ID to be used by default on Android. See <see cref="AdConfig.DefaultInterstitialAdUnitId" />.</param>
    /// <param name="iosDefaultBannerAdUnitId">Optional banner ad unit ID to be used by default on iOS. See <see cref="AdConfig.DefaultBannerAdUnitId" />.</param>
    /// <param name="iosDefaultInterstitialAdUnitId">Optional interstitial ad unit ID to be used by default on iOS. See <see cref="AdConfig.DefaultInterstitialAdUnitId" />.</param>
    /// <param name="androidDefaultRewardedAdUnitId">Optional rewarded ad unit ID to be used by default on Android. See <see cref="AdConfig.DefaultRewardedAdUnitId" />.</param>
    /// <param name="androidDefaultRewardedInterstitialAdUnitId">Optional interstitial rewarded ad unit ID to be used by default on Android. See <see cref="AdConfig.DefaultRewardedInterstitialAdUnitId" />.</param>
    /// <param name="iosDefaultRewardedAdUnitId">Optional rewarded ad unit ID to be used by default on iOS. See <see cref="AdConfig.DefaultRewardedAdUnitId" />.</param>
    /// <param name="iosDefaultRewardedInterstitialAdUnitId">Optional interstitial rewarded ad unit ID to be used by default on iOS. See <see cref="AdConfig.DefaultRewardedInterstitialAdUnitId" />.</param>
    /// <param name="androidDefaultAppOpenAdUnitId">Optional app open ad unit ID to be used by default on Android. See <see cref="AdConfig.DefaultAppOpenAdUnitId" />.</param>
    /// <param name="iosDefaultAppOpenAdUnitId">Optional app open ad unit ID to be used by default on Android. See <see cref="AdConfig.DefaultAppOpenAdUnitId" />.</param>
    /// <param name="androidDefaultNativeAdUnitId">Optional native ad unit ID to be used by default on Android. See <see cref="AdConfig.DefaultNativeAdUnitId" />.</param>
    /// <param name="iosDefaultNativeAdUnitId">Optional native ad unit ID to be used by default on Android. See <see cref="AdConfig.DefaultNativeAdUnitId" />.</param>
    /// <param name="disableConsentCheck">Default is false. When set to true, the plugin will no longer check if there is consent before making an ad request. You should use <see cref="IAdConsentService" /> to ask for consent and ensure you can request ads before making any ad request. If consent is missing, the Google Ads SDK will automatically drop the ad requests and no ads will be served.</param>
    /// <param name="automaticallyAskForConsent">Default is true. When set to false, the plugin will no longer ask for consent automatically. Use <see cref="IAdConsentService" /> to manage consent.</param>
    /// <param name="tagForChildDirectedTreatment">Indicate whether you want Google to treat your content as child-directed when you make an ad request. See <see cref="TagForChildDirectedTreatment" />.</param>
    /// <param name="tagForUnderAgeOfConsent">Indicate the treatment for users in the European Economic Area (EEA) under the age of consent. See <see cref="TagForUnderAgeOfConsent" />.</param>
    /// <param name="maxAdContentRating">Specifies the maximum ad content rating for your ad requests. AdMob ads returned when this is configured have a content rating at or below that level. See <see cref="MaxAdContentRating" />.</param>
    /// <returns>The source service collection.</returns>
    public static IServiceCollection AddAdMob( // Uno: was UseAdMob(this MauiAppBuilder builder, ...)
        this IServiceCollection services,
        string? androidDefaultBannerAdUnitId = null,
        string? androidDefaultInterstitialAdUnitId = null,
        string? iosDefaultBannerAdUnitId = null,
        string? iosDefaultInterstitialAdUnitId = null,
        string? androidDefaultRewardedAdUnitId = null,
        string? androidDefaultRewardedInterstitialAdUnitId = null,
        string? iosDefaultRewardedAdUnitId = null,
        string? iosDefaultRewardedInterstitialAdUnitId = null,
        string? androidDefaultAppOpenAdUnitId = null,
        string? iosDefaultAppOpenAdUnitId = null,
        string? androidDefaultNativeAdUnitId = null,
        string? iosDefaultNativeAdUnitId = null,
        bool disableConsentCheck = false,
        bool automaticallyAskForConsent = true,
        TagForChildDirectedTreatment tagForChildDirectedTreatment = TagForChildDirectedTreatment.None,
        TagForUnderAgeOfConsent tagForUnderAgeOfConsent = TagForUnderAgeOfConsent.None,
        MaxAdContentRating maxAdContentRating = MaxAdContentRating.None)
    {
#if ANDROID
        AdConfig.DefaultBannerAdUnitId = androidDefaultBannerAdUnitId ?? AdConfig.DefaultBannerAdUnitId;
        AdConfig.DefaultInterstitialAdUnitId = androidDefaultInterstitialAdUnitId ?? AdConfig.DefaultInterstitialAdUnitId;
        AdConfig.DefaultRewardedAdUnitId = androidDefaultRewardedAdUnitId ?? AdConfig.DefaultRewardedAdUnitId;
        AdConfig.DefaultRewardedInterstitialAdUnitId = androidDefaultRewardedInterstitialAdUnitId ?? AdConfig.DefaultRewardedInterstitialAdUnitId;
        AdConfig.DefaultAppOpenAdUnitId = androidDefaultAppOpenAdUnitId ?? AdConfig.DefaultAppOpenAdUnitId;
        AdConfig.DefaultNativeAdUnitId = androidDefaultNativeAdUnitId ?? AdConfig.DefaultNativeAdUnitId;
#elif IOS
        AdConfig.DefaultBannerAdUnitId = iosDefaultBannerAdUnitId ?? AdConfig.DefaultBannerAdUnitId;
        AdConfig.DefaultInterstitialAdUnitId = iosDefaultInterstitialAdUnitId ?? AdConfig.DefaultInterstitialAdUnitId;
        AdConfig.DefaultRewardedAdUnitId = iosDefaultRewardedAdUnitId ?? AdConfig.DefaultRewardedAdUnitId;
        AdConfig.DefaultRewardedInterstitialAdUnitId = iosDefaultRewardedInterstitialAdUnitId ?? AdConfig.DefaultRewardedInterstitialAdUnitId;
        AdConfig.DefaultAppOpenAdUnitId = iosDefaultAppOpenAdUnitId ?? AdConfig.DefaultAppOpenAdUnitId;
        AdConfig.DefaultNativeAdUnitId = iosDefaultNativeAdUnitId ?? AdConfig.DefaultNativeAdUnitId;
#endif

        if (disableConsentCheck)
        {
            AdConfig.DisableConsentCheck = true;
        }

        if (tagForChildDirectedTreatment is not TagForChildDirectedTreatment.None)
        {
            AdConfig.TagForChildDirectedTreatment = tagForChildDirectedTreatment;
        }

        if (tagForUnderAgeOfConsent is not TagForUnderAgeOfConsent.None)
        {
            AdConfig.TagForUnderAgeOfConsent = tagForUnderAgeOfConsent;
        }

        if (maxAdContentRating is not MaxAdContentRating.None)
        {
            AdConfig.MaxAdContentRating = maxAdContentRating;
        }

        // Uno: no MAUI handlers; banner and native views are not ported yet

        services.AddSingleton<IInterstitialAdService, InterstitialAdService>();
        services.AddSingleton<IRewardedAdService, RewardedAdService>();
        services.AddSingleton<IRewardedInterstitialAdService, RewardedInterstitialAdService>();
        services.AddSingleton<IAppOpenAdService, AppOpenAdService>();
        // Uno: INativeAdService registration removed, native ads not ported yet
        services.AddSingleton<IMobileAdsService, MobileAdsService>();

        var adConsentService = new AdConsentService();
        services.AddSingleton<IAdConsentService>(adConsentService);

        // Uno: consent/lifecycle hookup moved to AdMobPlatform (wired up in AdMobPlatform.Initialize)
        AdMobPlatform.AutomaticallyAskForConsent = automaticallyAskForConsent;

        return services;
    }

    /// <summary>
    /// Configures the consent debug settings to be used during testing.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="consentDebugSettings">The consent debug settings.</param>
    /// <returns>The source service collection.</returns>
    public static IServiceCollection AddConsentDebugSettings( // Uno: was UseConsentDebugSettings(this MauiAppBuilder builder, ...)
        this IServiceCollection services,
        ConsentDebugSettings consentDebugSettings
        )
    {
        ConsentDebugSettings.Current = consentDebugSettings;

        return services;
    }
}
