using Microsoft.Extensions.DependencyInjection;
using Uno.AdMob.Services;

namespace Uno.AdMob;

/// <summary>
/// Entry point that connects Uno.AdMob to the running app. Call <see cref="Initialize" /> after building your host.
/// </summary>
public static partial class AdMobPlatform
{
    /// <summary>
    /// True when ads can be served on the current platform (Android and iOS).
    /// </summary>
    public static bool IsSupported
    {
        get
        {
#if ANDROID || IOS
            return true;
#else
            return false;
#endif
        }
    }

    /// <summary>
    /// The service provider passed to <see cref="Initialize" />, or null if it was not called yet.
    /// </summary>
    public static IServiceProvider? Services { get; private set; }

    internal static bool AutomaticallyAskForConsent { get; set; } = true;

    /// <summary>
    /// Raised at the end of <see cref="Initialize" />, so controls loaded earlier can finish setting up.
    /// </summary>
    internal static event EventHandler? Initialized;

    /// <summary>
    /// Initializes Uno.AdMob with the app's service provider. Call it once, after building the host, on the UI thread.
    /// </summary>
    /// <param name="services">The service provider that has <c>AddAdMob</c> registered.</param>
    public static void Initialize(IServiceProvider services)
    {
        ArgumentNullException.ThrowIfNull(services);

        if (services.GetService<IAdConsentService>() is null)
        {
            throw new InvalidOperationException("Uno.AdMob services are not registered. Call services.AddAdMob() before AdMobPlatform.Initialize.");
        }

        Services = services;
        OnInitialized(services);
        Initialized?.Invoke(null, EventArgs.Empty);
    }

    internal static T GetRequiredService<T>()
        where T : notnull
    {
        if (Services is null)
        {
            throw new InvalidOperationException("Uno.AdMob is not initialized. Call AdMobPlatform.Initialize(serviceProvider) after building your host.");
        }

        return Services.GetRequiredService<T>();
    }

    static partial void OnInitialized(IServiceProvider services);
}
