using Microsoft.Extensions.DependencyInjection;
using Uno.AdMob.Configuration;
using Uno.AdMob.Services;

namespace Uno.AdMob.Tests;

[TestClass]
[DoNotParallelize]
public class UnsupportedPlatformTests
{
    private const string ExpectedMessage = "Ads are only supported on Android and iOS.";

    private SynchronizationContext? _originalContext;
    private ServiceProvider _provider = null!;

    [TestInitialize]
    public void Setup()
    {
        _originalContext = SynchronizationContext.Current;
        AdConfig.DisableConsentCheck = false;

        ServiceCollection services = new();
        services.AddAdMob();
        _provider = services.BuildServiceProvider();
    }

    [TestCleanup]
    public void Cleanup()
    {
        SynchronizationContext.SetSynchronizationContext(_originalContext);
        _provider.Dispose();
    }

    [TestMethod]
    public void Load_WithoutSyncContext_RaisesFailureInline()
    {
        SynchronizationContext.SetSynchronizationContext(null);
        var ad = _provider.GetRequiredService<IInterstitialAdService>().CreateAd("x");
        IAdError? error = null;
        ad.OnAdFailedToLoad += (_, e) => error = e;

        ad.Load();

        Assert.IsNotNull(error);
        Assert.AreEqual(ExpectedMessage, error.Message);
        Assert.IsFalse(ad.IsLoaded);
    }

    [TestMethod]
    public void Load_WithSyncContext_PostsFailure()
    {
        AssertPosted(
            _provider.GetRequiredService<IInterstitialAdService>().CreateAd("x"),
            (ad, handler) => ad.OnAdFailedToLoad += handler,
            ad => ad.Load());
        AssertPosted(
            _provider.GetRequiredService<IRewardedAdService>().CreateAd("x"),
            (ad, handler) => ad.OnAdFailedToLoad += handler,
            ad => ad.Load());
        AssertPosted(
            _provider.GetRequiredService<IRewardedInterstitialAdService>().CreateAd("x"),
            (ad, handler) => ad.OnAdFailedToLoad += handler,
            ad => ad.Load());
        AssertPosted(
            _provider.GetRequiredService<IAppOpenAdService>().CreateAd("x"),
            (ad, handler) => ad.OnAdFailedToLoad += handler,
            ad => ad.Load());
    }

    [TestMethod]
    public void Consent_CannotRequestAds()
    {
        var consent = _provider.GetRequiredService<IAdConsentService>();

        Assert.IsFalse(consent.CanRequestAds());
        Assert.IsFalse(consent.IsPrivacyOptionsRequired());
    }

    [TestMethod]
    public void MobileAdsService_Fallback_InitializationNeverCompletes()
    {
        var service = _provider.GetRequiredService<IMobileAdsService>();

        Assert.IsFalse(service.Initialization.IsCompleted);
    }

    private static void AssertPosted<TAd>(TAd ad, Action<TAd, EventHandler<IAdError>> subscribe, Action<TAd> load)
    {
        QueueSynchronizationContext context = new();
        SynchronizationContext.SetSynchronizationContext(context);
        var raised = 0;
        subscribe(ad, (_, e) =>
        {
            raised++;
            Assert.AreEqual(ExpectedMessage, e.Message);
        });

        load(ad);

        Assert.AreEqual(0, raised, typeof(TAd).Name);
        context.Drain();
        Assert.AreEqual(1, raised, typeof(TAd).Name);
    }

    private sealed class QueueSynchronizationContext : SynchronizationContext
    {
        private readonly Queue<(SendOrPostCallback Callback, object? State)> _queue = new();

        public override void Post(SendOrPostCallback d, object? state) => _queue.Enqueue((d, state));

        public void Drain()
        {
            while (_queue.Count > 0)
            {
                var (callback, state) = _queue.Dequeue();
                callback(state);
            }
        }
    }
}
