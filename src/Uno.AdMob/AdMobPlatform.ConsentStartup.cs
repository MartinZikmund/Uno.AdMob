#if ANDROID || IOS
using Microsoft.Extensions.DependencyInjection;
using Uno.AdMob.Services;

namespace Uno.AdMob;

public static partial class AdMobPlatform
{
    private static bool _consentRequested;
    private static IAdConsentService? _wiredConsentService;

    /// <summary>
    /// Starts the Mobile Ads SDK once consent allows ads, and re-arms the automatic consent flow if it fails.
    /// </summary>
    private static void SetUpConsent(IServiceProvider services)
    {
        var adConsentService = services.GetRequiredService<IAdConsentService>();
        if (ReferenceEquals(_wiredConsentService, adConsentService))
        {
            return;
        }

        _wiredConsentService = adConsentService;

        adConsentService.OnConsentInfoUpdated += (_, info) =>
        {
            if (info?.CanRequestAds is true)
            {
                MobileAdsInitialization.Start();
            }
        };

        if (AutomaticallyAskForConsent)
        {
            adConsentService.OnConsentInfoFailedToUpdate += (_, _) => _consentRequested = false;
            adConsentService.OnConsentFormError += (_, _) => _consentRequested = false;
        }
    }

    /// <summary>
    /// Loads and shows the consent form if required, only the first time it is called (until a failure resets it).
    /// </summary>
    private static void RunAutomaticConsentOnce()
    {
        if (_consentRequested)
        {
            return;
        }

        _consentRequested = true;
        GetRequiredService<IAdConsentService>().LoadAndShowConsentFormIfRequired();
    }
}
#endif
