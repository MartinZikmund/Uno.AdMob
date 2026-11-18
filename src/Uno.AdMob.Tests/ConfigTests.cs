using Microsoft.Extensions.DependencyInjection;
using Uno.AdMob.Configuration;
using Uno.AdMob.Services;

namespace Uno.AdMob.Tests;

[TestClass]
[DoNotParallelize]
public class ConfigTests
{
    [TestInitialize]
    public void Reset()
    {
        AdConfig.DisableConsentCheck = false;
        AdConfig.TagForChildDirectedTreatment = TagForChildDirectedTreatment.None;
        AdConfig.TagForUnderAgeOfConsent = TagForUnderAgeOfConsent.None;
        AdConfig.MaxAdContentRating = MaxAdContentRating.None;
    }

    [TestMethod]
    public void AddAdMob_RegistersServices()
    {
        ServiceCollection services = new();
        services.AddAdMob();
        var provider = services.BuildServiceProvider();

        AssertSingleton<IInterstitialAdService>(provider);
        AssertSingleton<IRewardedAdService>(provider);
        AssertSingleton<IRewardedInterstitialAdService>(provider);
        AssertSingleton<IAppOpenAdService>(provider);
        AssertSingleton<IMobileAdsService>(provider);
        AssertSingleton<IAdConsentService>(provider);

        Assert.IsNull(typeof(Config).Assembly.GetType("Uno.AdMob.Services.INativeAdService"));
    }

    [TestMethod]
    public void AddAdMob_AppliesFlags()
    {
        ServiceCollection services = new();
        services.AddAdMob(
            disableConsentCheck: true,
            tagForChildDirectedTreatment: TagForChildDirectedTreatment.True,
            tagForUnderAgeOfConsent: TagForUnderAgeOfConsent.False,
            maxAdContentRating: MaxAdContentRating.T);

        Assert.IsTrue(AdConfig.DisableConsentCheck);
        Assert.AreEqual(TagForChildDirectedTreatment.True, AdConfig.TagForChildDirectedTreatment);
        Assert.AreEqual(TagForUnderAgeOfConsent.False, AdConfig.TagForUnderAgeOfConsent);
        Assert.AreEqual(MaxAdContentRating.T, AdConfig.MaxAdContentRating);
    }

    private static void AssertSingleton<T>(IServiceProvider provider)
        where T : notnull
    {
        var first = provider.GetRequiredService<T>();
        var second = provider.GetRequiredService<T>();
        Assert.IsNotNull(first);
        Assert.AreSame(first, second);
    }
}
