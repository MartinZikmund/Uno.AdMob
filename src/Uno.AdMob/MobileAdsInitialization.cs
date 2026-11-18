#if ANDROID || IOS
#if ANDROID
using Uno.AdMob.Platforms.Android;
#else
using Uno.AdMob.Platforms.iOS;
#endif

namespace Uno.AdMob;

internal static class MobileAdsInitialization
{
    private static readonly TaskCompletionSource<IInitializationStatus> _completed = new(TaskCreationOptions.RunContinuationsAsynchronously);
    private static int _started;

    internal static Task<IInitializationStatus> Completed => _completed.Task;

    internal static void Start()
    {
        if (Interlocked.Exchange(ref _started, 1) == 1)
        {
            return;
        }

        AdMobInitializer.EnsureInitialized();
    }

    internal static void Complete(Func<IInitializationStatus> getStatus)
    {
        try
        {
            _completed.TrySetResult(getStatus());
        }
        catch (Exception ex)
        {
            _completed.TrySetException(ex);
        }
    }
}
#endif
