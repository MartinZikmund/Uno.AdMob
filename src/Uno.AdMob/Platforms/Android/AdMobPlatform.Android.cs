using Android.App;
using Application = Android.App.Application; // Uno: disambiguates from Microsoft.UI.Xaml.Application
using Uno.AdMob.Platforms.Android;

namespace Uno.AdMob;

public static partial class AdMobPlatform
{
    private static bool _lifecycleHooked;

    static partial void OnInitialized(IServiceProvider services)
    {
        SetUpConsent(services);

        if (Application.Context is Application app)
        {
            ActivityResolver.Register(app);
        }

        if (!AutomaticallyAskForConsent || _lifecycleHooked)
        {
            return;
        }

        _lifecycleHooked = true;
        ActivityResolver.ActivityStarted += _ => RunAutomaticConsentOnce();

        // Initialize usually runs after the first Activity has already started.
        if (ActivityResolver.Current is not null)
        {
            RunAutomaticConsentOnce();
        }
    }
}
