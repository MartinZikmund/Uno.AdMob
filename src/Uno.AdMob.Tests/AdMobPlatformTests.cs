using Microsoft.Extensions.DependencyInjection;
using Uno.AdMob.Services;

namespace Uno.AdMob.Tests;

[TestClass]
[DoNotParallelize]
public class AdMobPlatformTests
{
    [TestInitialize]
    [TestCleanup]
    public void ResetServices() => typeof(AdMobPlatform).GetProperty(nameof(AdMobPlatform.Services))!.SetValue(null, null);

    [TestMethod]
    public void IsSupported_FalseOnNet10() => Assert.IsFalse(AdMobPlatform.IsSupported);

    [TestMethod]
    public void GetRequiredService_BeforeInitialize_Throws()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => AdMobPlatform.GetRequiredService<IAdConsentService>());
        StringAssert.Contains(ex.Message, "AdMobPlatform.Initialize");
    }

    [TestMethod]
    public void Initialize_ThenGetRequiredService_Resolves()
    {
        ServiceCollection services = new();
        services.AddAdMob();
        var provider = services.BuildServiceProvider();

        AdMobPlatform.Initialize(provider);

        Assert.AreSame(provider, AdMobPlatform.Services);
        Assert.AreSame(provider.GetRequiredService<IAdConsentService>(), AdMobPlatform.GetRequiredService<IAdConsentService>());
    }

    [TestMethod]
    public void Initialize_RaisesInitializedOncePerCall()
    {
        ServiceCollection services = new();
        services.AddAdMob();
        var provider = services.BuildServiceProvider();
        int raised = 0;
        EventHandler handler = (_, _) => raised++;
        AdMobPlatform.Initialized += handler;

        try
        {
            AdMobPlatform.Initialize(provider);
            Assert.AreEqual(1, raised);

            AdMobPlatform.Initialize(provider);
            Assert.AreEqual(2, raised);
        }
        finally
        {
            AdMobPlatform.Initialized -= handler;
        }
    }

    [TestMethod]
    public void Initialize_WithoutAddAdMob_ThrowsAndDoesNotStoreServices()
    {
        var provider = new ServiceCollection().BuildServiceProvider();
        int raised = 0;
        EventHandler handler = (_, _) => raised++;
        AdMobPlatform.Initialized += handler;

        try
        {
            var ex = Assert.Throws<InvalidOperationException>(() => AdMobPlatform.Initialize(provider));
            StringAssert.Contains(ex.Message, "AddAdMob");
            Assert.IsNull(AdMobPlatform.Services);
            Assert.AreEqual(0, raised);
        }
        finally
        {
            AdMobPlatform.Initialized -= handler;
        }
    }

    [TestMethod]
    public void BannerAd_Type_IsContentControl() => Assert.AreEqual(typeof(Microsoft.UI.Xaml.Controls.ContentControl), typeof(BannerAd).BaseType);

    [TestMethod]
    public void BannerAd_Has_Upstream_Events()
    {
        foreach (var name in new[] { "OnAdLoaded", "OnAdFailedToLoad", "OnAdImpression", "OnAdClicked", "OnAdSwiped", "OnAdOpened", "OnAdClosed" })
        {
            Assert.IsNotNull(typeof(BannerAd).GetEvent(name), name);
        }
    }
}
