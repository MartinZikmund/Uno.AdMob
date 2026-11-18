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
}
