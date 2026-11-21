namespace Uno.AdMob;

// Uno: adaptive banners fill the width their parent gives them, not the whole window
internal static class AdaptiveBannerWidth
{
    public static double Resolve(double availableWidth, double windowWidth) =>
        double.IsFinite(availableWidth) && availableWidth > 0 ? Math.Min(availableWidth, windowWidth) : windowWidth;
}
