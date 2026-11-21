namespace Uno.AdMob.Tests;

[TestClass]
public class AdaptiveBannerWidthTests
{
    [TestMethod]
    public void Resolve_UsesAvailableWidth_WhenNarrowerThanWindow() =>
        Assert.AreEqual(1119, AdaptiveBannerWidth.Resolve(availableWidth: 1119, windowWidth: 1280));

    [TestMethod]
    public void Resolve_CapsAtWindowWidth() =>
        Assert.AreEqual(1280, AdaptiveBannerWidth.Resolve(availableWidth: 1500, windowWidth: 1280));

    [TestMethod]
    [DataRow(0d)]
    [DataRow(-1d)]
    [DataRow(double.NaN)]
    [DataRow(double.PositiveInfinity)]
    public void Resolve_FallsBackToWindowWidth_WhenAvailableWidthIsUnknown(double availableWidth) =>
        Assert.AreEqual(1280, AdaptiveBannerWidth.Resolve(availableWidth, windowWidth: 1280));
}
