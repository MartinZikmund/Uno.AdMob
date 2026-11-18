using Android.App;
using Application = Android.App.Application; // Uno: disambiguates from Microsoft.UI.Xaml.Application
using Android.OS;

namespace Uno.AdMob.Platforms.Android;

/// <summary>
/// Tracks the current Activity, replacing MAUI's ActivityStateManager.
/// </summary>
internal static class ActivityResolver
{
    private static readonly ActivityTracker _tracker = new();
    private static bool _registered;
    private static Activity? _current;

    /// <summary>
    /// Raised on the main thread whenever an Activity is started.
    /// </summary>
    internal static event Action<Activity>? ActivityStarted;

    /// <summary>
    /// The most recently started or resumed Activity, or Uno's current Activity if none was tracked yet.
    /// </summary>
    internal static Activity? Current => _current ?? Uno.UI.ContextHelper.Current as Activity;

    internal static void Register(Application app)
    {
        if (_registered)
        {
            return;
        }

        _registered = true;
        app.RegisterActivityLifecycleCallbacks(_tracker);
    }

    private sealed class ActivityTracker : Java.Lang.Object, Application.IActivityLifecycleCallbacks
    {
        public void OnActivityStarted(Activity activity)
        {
            _current = activity;
            ActivityStarted?.Invoke(activity);
        }

        public void OnActivityResumed(Activity activity) => _current = activity;

        public void OnActivityDestroyed(Activity activity)
        {
            if (ReferenceEquals(_current, activity))
            {
                _current = null;
            }
        }

        public void OnActivityCreated(Activity activity, Bundle? savedInstanceState)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}
