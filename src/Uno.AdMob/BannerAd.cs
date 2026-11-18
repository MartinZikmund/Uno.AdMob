using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
#if ANDROID || IOS
using Uno.AdMob.Handlers;
#endif

namespace Uno.AdMob;

/// <summary>
/// Displays a banner ad.
/// </summary>
public partial class BannerAd : ContentControl // Uno: ContentControl instead of MAUI ContentView
{
    /// <summary>
    /// Raised when an ad is received.
    /// </summary>
    public event EventHandler? OnAdLoaded;

    /// <summary>
    /// Raised when an ad request failed.
    /// </summary>
    public event EventHandler<IAdError>? OnAdFailedToLoad;

    /// <summary>
    /// Raised when an impression is recorded for an ad.
    /// </summary>
    public event EventHandler? OnAdImpression;

    /// <summary>
    /// Raised when a click is recorded for an ad.
    /// </summary>
    public event EventHandler? OnAdClicked;

    /// <summary>
    /// Raised when a swipe gesture on an ad is recorded as a click. Banners no longer report swipes on either platform: the Google Mobile Ads Next-Gen SDK used on Android has no swipe event for banners.
    /// </summary>
    public event EventHandler? OnAdSwiped;

    /// <summary>
    /// Raised when an ad opens an overlay that covers the screen.
    /// </summary>
    public event EventHandler? OnAdOpened;

    /// <summary>
    /// Raised when the user is about to return to the application after clicking on an ad.
    /// </summary>
    public event EventHandler? OnAdClosed;

    // Uno: DependencyProperty instead of BindableProperty (all properties below); internal because IsLoaded is read-only
    /// <summary>
    /// Determines whether the ad is loaded or not.
    /// </summary>
    internal static readonly DependencyProperty IsLoadedProperty =
        DependencyProperty.Register(nameof(IsLoaded), typeof(bool), typeof(BannerAd), new PropertyMetadata(false));

    /// <summary>
    /// Determines whether the ad is loaded or not.
    /// </summary>
    public new bool IsLoaded
    {
        get { return (bool)GetValue(IsLoadedProperty); }
        private set { SetValue(IsLoadedProperty, value); }
    }

    /// <summary>
    /// The ad unit id.
    /// </summary>
    public static readonly DependencyProperty AdUnitIdProperty =
        DependencyProperty.Register(nameof(AdUnitId), typeof(string), typeof(BannerAd), new PropertyMetadata(null));

    /// <summary>
    /// The ad unit id.
    /// </summary>
    public string? AdUnitId
    {
        get { return (string?)GetValue(AdUnitIdProperty); }
        set { SetValue(AdUnitIdProperty, value); }
    }

    /// <summary>
    /// The desired ad size.
    /// </summary>
    public static readonly DependencyProperty AdSizeProperty =
        DependencyProperty.Register(nameof(AdSize), typeof(AdSize?), typeof(BannerAd), new PropertyMetadata(null));

    /// <summary>
    /// The desired ad size.
    /// </summary>
    public AdSize? AdSize
    {
        get { return (AdSize?)GetValue(AdSizeProperty); }
        set { SetValue(AdSizeProperty, value); }
    }

    /// <summary>
    /// The desired ad width in density-independent pixels. Used when <see cref="AdSize" /> is set to <see cref="AdMob.AdSize.Custom" />.
    /// </summary>
    public static readonly DependencyProperty AdWidthProperty =
        DependencyProperty.Register("AdWidth", typeof(int), typeof(BannerAd), new PropertyMetadata(0));

    /// <summary>
    /// The desired ad width in density-independent pixels. Used when <see cref="AdSize" /> is set to <see cref="AdMob.AdSize.Custom" />.
    /// </summary>
    public int CustomAdWidth
    {
        get { return (int)GetValue(AdWidthProperty); }
        set { SetValue(AdWidthProperty, value); }
    }

    /// <summary>
    /// The desired ad height in density-independent pixels. Used when <see cref="AdSize" /> is set to <see cref="AdMob.AdSize.Custom" />.
    /// </summary>
    public static readonly DependencyProperty AdHeightProperty =
        DependencyProperty.Register("AdHeight", typeof(int), typeof(BannerAd), new PropertyMetadata(0));

    /// <summary>
    /// The desired ad height in density-independent pixels. Used when <see cref="AdSize" /> is set to <see cref="AdMob.AdSize.Custom" />.
    /// </summary>
    public int CustomAdHeight
    {
        get { return (int)GetValue(AdHeightProperty); }
        set { SetValue(AdHeightProperty, value); }
    }

    // Uno: lets the ported handlers keep MAUI's WidthRequest/HeightRequest calls
    internal double WidthRequest { set => Width = value; }

    internal double HeightRequest { set => Height = value; }

#if ANDROID || IOS
    // Uno: hosts the native ad view via the handler, replacing MAUI's handler pipeline
    private BannerAdHandler? _handler;
    private bool _waitingForInitialize;

    /// <inheritdoc />
    public BannerAd()
    {
        // Uno: native content defaults to Left/Top inside ContentControl, stretch it to fill the banner
        HorizontalContentAlignment = HorizontalAlignment.Stretch;
        VerticalContentAlignment = VerticalAlignment.Stretch;
        Loaded += OnBannerLoaded;
        Unloaded += OnBannerUnloaded;
    }

    private void OnBannerLoaded(object sender, RoutedEventArgs e)
    {
        if (_handler is not null)
        {
            return;
        }

        if (AdMobPlatform.Services is null)
        {
            // Uno: the host may navigate to the first page before the app calls AdMobPlatform.Initialize
            if (!_waitingForInitialize)
            {
                _waitingForInitialize = true;
                AdMobPlatform.Initialized += OnPlatformInitialized;
            }

            return;
        }

        CreateHandler();
    }

    private void OnPlatformInitialized(object? sender, EventArgs e)
    {
        StopWaitingForInitialize();

        if (DispatcherQueue.HasThreadAccess)
        {
            CreateHandler();
        }
        else
        {
            DispatcherQueue.TryEnqueue(CreateHandler);
        }
    }

    private void StopWaitingForInitialize()
    {
        if (_waitingForInitialize)
        {
            _waitingForInitialize = false;
            AdMobPlatform.Initialized -= OnPlatformInitialized;
        }
    }

    private void CreateHandler()
    {
        // Uno: base.IsLoaded is the element-in-tree flag, BannerAd.IsLoaded means "ad received"
        if (_handler is not null || !base.IsLoaded)
        {
            return;
        }

        BannerAdHandler handler = new();
        handler.Connect(this);
        _handler = handler;
        Content = handler.PlatformView;
    }

    private void OnBannerUnloaded(object sender, RoutedEventArgs e)
    {
        StopWaitingForInitialize();
        Content = null;
        _handler?.Disconnect();
        _handler = null;
    }

    /// <inheritdoc />
    protected override global::Windows.Foundation.Size ArrangeOverride(global::Windows.Foundation.Size finalSize)
    {
        _handler?.PlatformArrange(new global::Windows.Foundation.Rect(0, 0, finalSize.Width, finalSize.Height));
        return base.ArrangeOverride(finalSize);
    }
#else
    private bool _unsupportedReported;

    /// <inheritdoc />
    public BannerAd()
    {
        Width = 0;
        Height = 0;
        Loaded += OnBannerLoaded;
    }

    private void OnBannerLoaded(object sender, RoutedEventArgs e)
    {
        if (_unsupportedReported)
        {
            return;
        }

        _unsupportedReported = true;
        UnsupportedPlatform.PostLoadFailure(() => RaiseOnAdFailedToLoad(this, new AdError(AdError.UnsupportedPlatformMessage)));
    }
#endif

    internal void RaiseOnAdLoaded(object? sender, EventArgs e)
    {
        IsLoaded = true;
        OnAdLoaded?.Invoke(this, e);
    }

    internal void RaiseOnAdFailedToLoad(object? sender, IAdError e)
    {
        IsLoaded = false;
        OnAdFailedToLoad?.Invoke(this, e);
    }

    internal void RaiseOnAdImpression(object? sender, EventArgs e) => OnAdImpression?.Invoke(this, e);
    internal void RaiseOnAdClicked(object? sender, EventArgs e) => OnAdClicked?.Invoke(this, e);
    internal void RaiseOnAdSwiped(object? sender, EventArgs e) => OnAdSwiped?.Invoke(this, e);
    internal void RaiseOnAdOpened(object? sender, EventArgs e) => OnAdOpened?.Invoke(this, e);
    internal void RaiseOnAdClosed(object? sender, EventArgs e) => OnAdClosed?.Invoke(this, e);
}
