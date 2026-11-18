using Foundation;
using UIKit;

namespace Uno.AdMob;

public static partial class AdMobPlatform
{
    private static NSObject? _didBecomeActiveObserver;

    static partial void OnInitialized(IServiceProvider services)
    {
        SetUpConsent(services);

        if (!AutomaticallyAskForConsent || _didBecomeActiveObserver is not null)
        {
            return;
        }

        _didBecomeActiveObserver = UIApplication.Notifications.ObserveDidBecomeActive((_, _) => RunAutomaticConsentOnce());

        // Initialize usually runs after the app has already become active.
        if (UIApplication.SharedApplication.ApplicationState == UIApplicationState.Active)
        {
            RunAutomaticConsentOnce();
        }
    }
}
