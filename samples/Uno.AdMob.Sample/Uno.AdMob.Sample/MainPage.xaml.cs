using Uno.AdMob;

namespace Uno.AdMob.Sample;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();

        SupportedText.Text = $"AdMobPlatform.IsSupported = {AdMobPlatform.IsSupported}";

        Banner.OnAdLoaded += (s, e) => Report("OnAdLoaded");
        Banner.OnAdFailedToLoad += (s, e) => Report($"OnAdFailedToLoad: {e.Message}");
        Banner.OnAdImpression += (s, e) => Report("OnAdImpression");
        Banner.OnAdClicked += (s, e) => Report("OnAdClicked");
        Banner.OnAdOpened += (s, e) => Report("OnAdOpened");
        Banner.OnAdClosed += (s, e) => Report("OnAdClosed");
    }

    private void Report(string text) => EventText.Text = $"Last banner event: {text}";
}
