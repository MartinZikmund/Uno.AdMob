#if ANDROID || IOS
using Windows.Foundation;
#if ANDROID
using Android.App;
using Application = Android.App.Application;
using Uno.AdMob.Platforms.Android;
#endif

namespace Uno.AdMob.Handlers;

/// <summary>
/// Minimal stand-in for MAUI's <c>ViewHandler</c>, so the ported banner handlers compile nearly unchanged.
/// </summary>
internal abstract class ViewHandler<TView, TPlatformView>
    where TView : Microsoft.UI.Xaml.FrameworkElement
    where TPlatformView : class
{
    private TView? _virtualView;
    private TPlatformView? _platformView;

    public TView VirtualView => _virtualView ?? throw new InvalidOperationException("VirtualView cannot be null here");

    public TPlatformView PlatformView => _platformView ?? throw new InvalidOperationException("PlatformView cannot be null here");

#if ANDROID
    protected global::Android.Content.Context Context => ActivityResolver.Current ?? Application.Context;
#endif

    protected abstract TPlatformView CreatePlatformView();

    protected virtual void ConnectHandler(TPlatformView platformView)
    {
    }

    protected virtual void DisconnectHandler(TPlatformView platformView)
    {
    }

    public virtual void PlatformArrange(Rect rect)
    {
    }

    internal void Connect(TView view)
    {
        _virtualView = view;
        _platformView = CreatePlatformView();
        ConnectHandler(_platformView);
    }

    internal void Disconnect()
    {
        if (_platformView is { } platformView)
        {
            DisconnectHandler(platformView);
        }

        _platformView = null;
        _virtualView = null;
    }
}
#endif
