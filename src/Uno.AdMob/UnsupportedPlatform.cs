#if !ANDROID && !IOS
namespace Uno.AdMob.Services
{
    internal partial class AdConsentService
    {
        public void LoadAndShowConsentFormIfRequired() { }

        public bool CanRequestAds() => false;

        public bool IsPrivacyOptionsRequired() => false;

        public void ShowPrivacyOptionsForm() { }

        public void Reset() { }
    }
}

namespace Uno.AdMob
{
    internal static class UnsupportedPlatform
    {
        internal static void PostLoadFailure(Action raise)
        {
            // Uno: SynchronizationContext instead of the MAUI dispatcher
            if (SynchronizationContext.Current is { } context)
            {
                context.Post(_ => raise(), null);
                return;
            }

            raise();
        }
    }

    internal partial class InterstitialAd
    {
        public void Load() => UnsupportedPlatform.PostLoadFailure(() => OnAdFailedToLoad?.Invoke(this, new AdError(AdError.UnsupportedPlatformMessage)));

        public void Show() { }
    }

    internal partial class RewardedAd
    {
        public void Load() => UnsupportedPlatform.PostLoadFailure(() => OnAdFailedToLoad?.Invoke(this, new AdError(AdError.UnsupportedPlatformMessage)));

        public void Show() { }
    }

    internal partial class RewardedInterstitialAd
    {
        public void Load() => UnsupportedPlatform.PostLoadFailure(() => OnAdFailedToLoad?.Invoke(this, new AdError(AdError.UnsupportedPlatformMessage)));

        public void Show() { }
    }

    internal partial class AppOpenAd
    {
        public void Load() => UnsupportedPlatform.PostLoadFailure(() => OnAdFailedToLoad?.Invoke(this, new AdError(AdError.UnsupportedPlatformMessage)));

        public void Show() { }
    }

    // Uno: native ads not ported yet
}
#endif
