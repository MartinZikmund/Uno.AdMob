# Upstream sync

Uno.AdMob mirrors [marius-bughiu/Plugin.AdMob](https://github.com/marius-bughiu/Plugin.AdMob).

- Synced tag: `v11.0.0.0`
- Synced commit: `1efa92e2db8a73e2119ce334148a4e3bc604f07b` (tag object `2e1bc29d84bd11290fec314dd57f89b665064ef5`)

## How to sync

1. Clone upstream next to this repo and fetch the new tag.
2. See what changed: `git -C <upstream> diff <old>..<new> -- src/Plugin.AdMob`
3. Go through the table below row by row. Apply each upstream change to the matching Uno path, keeping the `// Uno:` comments.
   - `ported`: copy the change, rename the namespace `Plugin.AdMob` to `Uno.AdMob`.
   - `adapted`: re-apply the change by hand, the file differs on purpose.
   - `pending`: not ported yet, nothing to sync until it lands.
   - `skipped`: ignore, unless we decide to port it.
4. Add rows for new upstream files, then update the synced tag and commit above.

## File map

| Upstream path | Uno path | Status | Note |
|---|---|---|---|
| `.github/workflows/binding-drift.yml` | - | skipped | Upstream CI; Uno.AdMob has its own workflows. |
| `.github/workflows/build.yml` | - | skipped | Upstream CI; Uno.AdMob has its own workflows. |
| `.github/workflows/bump-maui.yml` | - | skipped | Upstream CI; Uno.AdMob has its own workflows. |
| `.github/workflows/device-tests.yml` | - | skipped | Upstream CI; Uno.AdMob has its own workflows. |
| `.github/workflows/nightly.yml` | - | skipped | Upstream CI; Uno.AdMob has its own workflows. |
| `.github/workflows/release.yml` | - | skipped | Upstream CI; Uno.AdMob has its own workflows. |
| `.github/workflows/sync-wiki.yml` | - | skipped | Upstream CI; Uno.AdMob has its own workflows. |
| `samples/Foo.Bar.SampleApp.Hybrid/App.xaml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/App.xaml.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Components/Layout/MainLayout.razor` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Components/Layout/MainLayout.razor.css` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Components/Layout/NavMenu.razor` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Components/Layout/NavMenu.razor.css` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Components/Pages/Counter.razor` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Components/Pages/Home.razor` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Components/Pages/Weather.razor` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Components/Routes.razor` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Components/_Imports.razor` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Foo.Bar.SampleApp.Hybrid.csproj` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/MainPage.xaml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/MainPage.xaml.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/MauiProgram.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/Android/AndroidManifest.xml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/Android/MainActivity.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/Android/MainApplication.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/Android/Resources/values/colors.xml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/MacCatalyst/AppDelegate.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/MacCatalyst/Entitlements.plist` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/MacCatalyst/Info.plist` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/MacCatalyst/Program.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/Tizen/Main.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/Tizen/tizen-manifest.xml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/Windows/App.xaml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/Windows/App.xaml.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/Windows/Package.appxmanifest` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/Windows/app.manifest` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/iOS/AppDelegate.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/iOS/Info.plist` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/iOS/Program.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/iOS/Resources/PrivacyInfo.xcprivacy` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Platforms/iOS/SceneDelegate.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Properties/launchSettings.json` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Resources/AppIcon/appicon.svg` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Resources/AppIcon/appiconfg.svg` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Resources/Fonts/OpenSans-Regular.ttf` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Resources/Images/dotnet_bot.svg` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Resources/Raw/AboutAssets.txt` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/Resources/Splash/splash.svg` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/wwwroot/css/app.css` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/wwwroot/css/bootstrap/bootstrap.min.css` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/wwwroot/css/bootstrap/bootstrap.min.css.map` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/wwwroot/favicon.png` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp.Hybrid/wwwroot/index.html` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/App.xaml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/App.xaml.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/AppShell.xaml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/AppShell.xaml.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Foo.Bar.SampleApp.csproj` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/MainPage.xaml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/MainPage.xaml.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/MauiProgram.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Pages/NativeAdsPage.xaml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Pages/NativeAdsPage.xaml.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Platforms/Android/AndroidManifest.xml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Platforms/Android/MainActivity.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Platforms/Android/MainApplication.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Platforms/Android/Resources/values/colors.xml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Platforms/iOS/AppDelegate.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Platforms/iOS/Info.plist` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Platforms/iOS/Program.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Platforms/iOS/SceneDelegate.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Properties/launchSettings.json` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Resources/AppIcon/appicon.svg` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Resources/AppIcon/appiconfg.svg` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Resources/Fonts/OpenSans-Regular.ttf` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Resources/Fonts/OpenSans-Semibold.ttf` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Resources/Images/dotnet_bot.svg` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Resources/Raw/AboutAssets.txt` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Resources/Splash/splash.svg` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Resources/Styles/Colors.xaml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Resources/Styles/Styles.xaml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Services/ServiceProvider.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Views/MyCustomAdTemplate.xaml` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Foo.Bar.SampleApp/Views/MyCustomAdTemplate.xaml.cs` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `samples/Samples.sln` | - | skipped | Upstream MAUI samples; Uno sample is separate. |
| `src/Plugin.AdMob/AdError.cs` | `src/Uno.AdMob/AdError.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/AdSize.cs` | `src/Uno.AdMob/AdSize.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/AppOpenAd.cs` | `src/Uno.AdMob/AppOpenAd.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/BannerAd.cs` | `src/Uno.AdMob/BannerAd.cs` | adapted | ContentView becomes ContentControl, BindableProperty becomes DependencyProperty, handler hosting and unsupported fallback added. |
| `src/Plugin.AdMob/Config.cs` | `src/Uno.AdMob/Config.cs` | adapted | `UseAdMob(MauiAppBuilder)` became `AddAdMob(IServiceCollection)`; MAUI handlers and the native service registration dropped; consent/lifecycle hookup moved to `AdMobPlatform`. |
| `src/Plugin.AdMob/Configuration/AdConfig.cs` | `src/Uno.AdMob/Configuration/AdConfig.cs` | adapted | Only the `DefaultNativeAdUnitId` XML doc ("Reserved") changed. |
| `src/Plugin.AdMob/Configuration/AdMobTestAdUnits.cs` | `src/Uno.AdMob/Configuration/AdMobTestAdUnits.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Configuration/ConsentDebugSettings.cs` | `src/Uno.AdMob/Configuration/ConsentDebugSettings.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Configuration/VideoOptions.cs` | - | skipped | Native ads are not ported in v1. |
| `src/Plugin.AdMob/InitializationStatus.cs` | `src/Uno.AdMob/InitializationStatus.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/InterstitialAd.cs` | `src/Uno.AdMob/InterstitialAd.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/MaxAdContentRating.cs` | `src/Uno.AdMob/MaxAdContentRating.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/MediaView.cs` | - | skipped | Native ads are not ported in v1. |
| `src/Plugin.AdMob/MobileAdsInitialization.cs` | `src/Uno.AdMob/MobileAdsInitialization.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/NativeAd.cs` | - | skipped | Native ads are not ported in v1. |
| `src/Plugin.AdMob/NativeAdView.cs` | - | skipped | Native ads are not ported in v1. |
| `src/Plugin.AdMob/Platforms/Android/AdLoadCallback.cs` | `src/Uno.AdMob/Platforms/Android/AdLoadCallback.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Platforms/Android/AdMobInitializer.cs` | `src/Uno.AdMob/Platforms/Android/AdMobInitializer.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Platforms/Android/AdMobTestAdUnits.cs` | `src/Uno.AdMob/Platforms/Android/AdMobTestAdUnits.cs` | adapted | Native entries get "Reserved – native ads are not supported yet." XML doc. |
| `src/Plugin.AdMob/Platforms/Android/AppOpen/AppOpenAd.cs` | `src/Uno.AdMob/Platforms/Android/AppOpen/AppOpenAd.cs` | adapted | ActivityStateManager replaced by ActivityResolver.Current. |
| `src/Plugin.AdMob/Platforms/Android/Banner/BannerAdEventCallback.cs` | `src/Uno.AdMob/Platforms/Android/Banner/BannerAdEventCallback.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Platforms/Android/Banner/BannerAdHandler.cs` | `src/Uno.AdMob/Platforms/Android/Banner/BannerAdHandler.cs` | adapted | Runs on the minimal `Handlers/ViewHandler.cs`; mappers removed, WinUI `Rect`, `AdMobPlatform.GetRequiredService`. |
| `src/Plugin.AdMob/Platforms/Android/Banner/BannerAdRefreshCallback.cs` | `src/Uno.AdMob/Platforms/Android/Banner/BannerAdRefreshCallback.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Platforms/Android/Consent/AdConsentService.cs` | `src/Uno.AdMob/Platforms/Android/Consent/AdConsentService.cs` | adapted | ActivityStateManager replaced by ActivityResolver.Current. |
| `src/Plugin.AdMob/Platforms/Android/Extensions.cs` | `src/Uno.AdMob/Platforms/Android/Extensions.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Platforms/Android/FullScreenContentCallback.cs` | `src/Uno.AdMob/Platforms/Android/FullScreenContentCallback.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Platforms/Android/Interstitial/InterstitialAd.cs` | `src/Uno.AdMob/Platforms/Android/Interstitial/InterstitialAd.cs` | adapted | ActivityStateManager replaced by ActivityResolver.Current. |
| `src/Plugin.AdMob/Platforms/Android/MainThreadDispatcher.cs` | `src/Uno.AdMob/Platforms/Android/MainThreadDispatcher.cs` | adapted | MAUI MainThread replaced by Looper/Handler. |
| `src/Plugin.AdMob/Platforms/Android/Native/MediaViewHandler.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/Android/Native/NativeAd.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/Android/Native/NativeAdEventCallback.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/Android/Native/NativeAdHandler.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/Android/Native/NativeAdLoaderCallback.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/Android/Native/VideoLifecycleCallbacks.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/Android/Rewarded/RewardedAd.cs` | `src/Uno.AdMob/Platforms/Android/Rewarded/RewardedAd.cs` | adapted | ActivityStateManager replaced by ActivityResolver.Current. |
| `src/Plugin.AdMob/Platforms/Android/RewardedInterstitial/RewardedInterstitialAd.cs` | `src/Uno.AdMob/Platforms/Android/RewardedInterstitial/RewardedInterstitialAd.cs` | adapted | ActivityStateManager replaced by ActivityResolver.Current. |
| `src/Plugin.AdMob/Platforms/Android/UserEarnedRewardListener.cs` | `src/Uno.AdMob/Platforms/Android/UserEarnedRewardListener.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Platforms/MacCatalyst/Banner/BannerAdHandler.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/MacCatalyst/Native/MediaViewHandler.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/MacCatalyst/Native/NativeAdHandler.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/Windows/Banner/BannerAdHandler.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/Windows/Native/MediaViewHandler.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/Windows/Native/NativeAdHandler.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/iOS/AdMobInitializer.cs` | `src/Uno.AdMob/Platforms/iOS/AdMobInitializer.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Platforms/iOS/AppOpen/AppOpen.cs` | `src/Uno.AdMob/Platforms/iOS/AppOpen/AppOpen.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Platforms/iOS/Banner/BannerAdHandler.cs` | `src/Uno.AdMob/Platforms/iOS/Banner/BannerAdHandler.cs` | adapted | MAUI handler base replaced by internal `ViewHandler`; DI lookup via `AdMobPlatform`; key-window lookup instead of MAUI window handler (`// Uno:` lines). |
| `src/Plugin.AdMob/Platforms/iOS/Configuration/AdMobTestAdUnits.cs` | `src/Uno.AdMob/Platforms/iOS/Configuration/AdMobTestAdUnits.cs` | ported | Namespace rename; native entries documented as reserved. |
| `src/Plugin.AdMob/Platforms/iOS/Consent/AdConsentService.cs` | `src/Uno.AdMob/Platforms/iOS/Consent/AdConsentService.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Platforms/iOS/Extensions.cs` | `src/Uno.AdMob/Platforms/iOS/Extensions.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Platforms/iOS/Interstitial/InterstitialAd.cs` | `src/Uno.AdMob/Platforms/iOS/Interstitial/InterstitialAd.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Platforms/iOS/Native/MediaViewHandler.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/iOS/Native/NativeAd.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/iOS/Native/NativeAdHandler.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/iOS/Native/VideoControllerDelegate.cs` | - | skipped | Native ads / MacCatalyst / Windows are not ported in v1. |
| `src/Plugin.AdMob/Platforms/iOS/Rewarded/RewardedAd.cs` | `src/Uno.AdMob/Platforms/iOS/Rewarded/RewardedAd.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Platforms/iOS/RewardedInterstitial/RewardedInterstitialAd.cs` | `src/Uno.AdMob/Platforms/iOS/RewardedInterstitial/RewardedInterstitialAd.cs` | ported | Namespace rename only. |
| `src/Plugin.AdMob/Plugin.AdMob.csproj` | `src/Uno.AdMob/Uno.AdMob.csproj` | adapted | Rewritten for Uno.Sdk with central package management; versions come from `Directory.Packages.props`. |
| `src/Plugin.AdMob/RewardItem.cs` | `src/Uno.AdMob/RewardItem.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/RewardedAd.cs` | `src/Uno.AdMob/RewardedAd.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/RewardedInterstitialAd.cs` | `src/Uno.AdMob/RewardedInterstitialAd.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Services/AppOpenAdService.cs` | `src/Uno.AdMob/Services/AppOpenAdService.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Services/Consent/AdConsentService.cs` | `src/Uno.AdMob/Services/Consent/AdConsentService.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Services/Consent/ConsentError.cs` | `src/Uno.AdMob/Services/Consent/ConsentError.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Services/Consent/ConsentGate.cs` | `src/Uno.AdMob/Services/Consent/ConsentGate.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Services/Consent/ConsentInformation.cs` | `src/Uno.AdMob/Services/Consent/ConsentInformation.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Services/InterstitialAdService.cs` | `src/Uno.AdMob/Services/InterstitialAdService.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Services/MobileAdsService.cs` | `src/Uno.AdMob/Services/MobileAdsService.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Services/NativeAdService.cs` | - | skipped | Native ads are not ported in v1. |
| `src/Plugin.AdMob/Services/RewardedAdService.cs` | `src/Uno.AdMob/Services/RewardedAdService.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/Services/RewardedInterstitialAdService.cs` | `src/Uno.AdMob/Services/RewardedInterstitialAdService.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/TagForChildDirectedTreatment.cs` | `src/Uno.AdMob/TagForChildDirectedTreatment.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/TagForUnderAgeOfConsent.cs` | `src/Uno.AdMob/TagForUnderAgeOfConsent.cs` | ported | Namespace `Plugin.AdMob` to `Uno.AdMob` only. |
| `src/Plugin.AdMob/UnsupportedPlatform.cs` | `src/Uno.AdMob/UnsupportedPlatform.cs` | adapted | `PostLoadFailure` posts via `SynchronizationContext`; `NativeAd` stub removed. |
| `src/Plugin.AdMob/buildTransitive/Plugin.AdMob.proguard.cfg` | `src/Uno.AdMob/buildTransitive/MZikmund.Uno.AdMob.proguard.cfg` | ported | Renamed to the package id. |
| `src/Plugin.AdMob/buildTransitive/Plugin.AdMob.targets` | `src/Uno.AdMob/buildTransitive/MZikmund.Uno.AdMob.targets` | ported | Renamed to the package id. |
| `tests/Plugin.AdMob.DeviceTests/AdLoadHarness.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/App.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/EarlyLoads.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/HarnessConsent.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/HarnessLog.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/HarnessPage.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/MauiProgram.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/Platforms/Android/AndroidManifest.xml` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/Platforms/Android/MainActivity.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/Platforms/Android/MainApplication.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/Platforms/iOS/AppDelegate.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/Platforms/iOS/Info.plist` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/Platforms/iOS/Program.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/Platforms/iOS/SceneDelegate.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/Plugin.AdMob.DeviceTests.csproj` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/Resources/AppIcon/appicon.svg` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/Resources/AppIcon/appiconfg.svg` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.DeviceTests/Resources/Splash/splash.svg` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.PackageConsumer/ApiSurface.cs` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/Plugin.AdMob.PackageConsumer/Plugin.AdMob.PackageConsumer.csproj` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/README.md` | - | skipped | Upstream tests are MAUI device tests. |
| `tests/ci/run-android-harness.sh` | - | skipped | Upstream tests are MAUI device tests. |
| `docs/App-open-ads.md` | `docs/App-open-ads.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Banner-ads.md` | `docs/Banner-ads.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Configuration.md` | `docs/Configuration.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Consent.md` | `docs/Consent.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Home.md` | `docs/Home.md` | adapted | Native ads entry removed; Samples entry dropped. |
| `docs/Interstitial-ads.md` | `docs/Interstitial-ads.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Rewarded-ads.md` | `docs/Rewarded-ads.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Rewarded-interstitial-ads.md` | `docs/Rewarded-interstitial-ads.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Setup.md` | `docs/Setup.md` | adapted | UseAdMob -> AddAdMob + AdMobPlatform.Initialize; net11 -> net10; MAUI/MacCatalyst text replaced by Uno platform notes. |
| `docs/Troubleshooting.md` | `docs/Troubleshooting.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Troubleshooting-Ads-not-showing.md` | `docs/Troubleshooting-Ads-not-showing.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Troubleshooting-Android.md` | `docs/Troubleshooting-Android.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Troubleshooting-Banner-ads.md` | `docs/Troubleshooting-Banner-ads.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Troubleshooting-Consent.md` | `docs/Troubleshooting-Consent.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Troubleshooting-Interstitial-ads.md` | `docs/Troubleshooting-Interstitial-ads.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Troubleshooting-iOS.md` | `docs/Troubleshooting-iOS.md` | adapted | MAUI wording and snippets converted to Uno (AddAdMob, using: xmlns, AdMobPlatform.Services). |
| `docs/Upgrading.md` | `docs/Upgrading.md` | adapted | Replaced by a short page: migration table from Plugin.AdMob. Upstream release notes are MAUI-specific; port only API-behavior notes. |
| `docs/Native-ads.md` | - | skipped | Native ads are not supported in v1. |
| `docs/Troubleshooting-Native-ads.md` | - | skipped | Native ads are not supported in v1. |
| `docs/Troubleshooting-Blazor-Hybrid.md` | - | skipped | Blazor Hybrid is MAUI-only. |
| `docs/Troubleshooting-Upgrades-and-target-frameworks.md` | - | skipped | About MAUI target frameworks; Upgrading.md covers what applies. |
| `docs/Samples.md` | - | skipped | Upstream screenshots; Uno has its own sample. |
| `docs/_Sidebar.md` | - | skipped | Wiki sidebar; Uno docs are browsed on GitHub, Home.md has the index. |
| `README.md` | `README.md` | adapted | Rewritten for Uno (modelled on Uno.RevenueCat), credits upstream. |
| - | `src/Uno.AdMob/AdMobPlatform.cs` | adapted | Uno-only entry point (`Initialize`, `IsSupported`). Holds the consent/lifecycle hookup that upstream keeps in `Config.UseAdMob`. |
