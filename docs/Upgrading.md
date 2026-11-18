This page lists what to change in your app when you update `MZikmund.Uno.AdMob`, newest release first.

## Uno.AdMob 1.0

This is the first release. It is a port of [Plugin.AdMob](https://github.com/marius-bughiu/Plugin.AdMob) 11.0.0.0 (the synced upstream version is tracked in [UPSTREAM.md](../UPSTREAM.md)), so there is nothing to upgrade from. The upstream upgrade notes only apply to .NET MAUI apps, so they are not copied here.

### Moving from Plugin.AdMob (.NET MAUI) to Uno.AdMob

The ad APIs are the same on purpose. What changes is how you plug the library into your app.

| Plugin.AdMob (MAUI) | Uno.AdMob |
|---|---|
| `builder.UseAdMob(...)` in `MauiProgram.cs` | `services.AddAdMob(...)` (same parameters) |
| `builder.UseConsentDebugSettings(...)` | `services.AddConsentDebugSettings(...)` |
| (automatic) | `AdMobPlatform.Initialize(serviceProvider)` once, after building the host |
| `IPlatformApplication.Current.Services` | your own `IServiceProvider`, or `AdMobPlatform.Services` after `Initialize` |
| `xmlns:admob="clr-namespace:Plugin.AdMob;assembly=Plugin.AdMob"` | `xmlns:admob="using:Uno.AdMob"` |
| namespaces `Plugin.AdMob`, `Plugin.AdMob.Configuration`, `Plugin.AdMob.Services` | `Uno.AdMob`, `Uno.AdMob.Configuration`, `Uno.AdMob.Services` |
| `BannerAd` is a `ContentView`, `IsVisible` | `BannerAd` is a `ContentControl`, `Visibility` |
| `net11.0-android` / `net11.0-ios` | `net10.0-android` / `net10.0-ios` (Uno Platform 7.0) |

Not available yet:

- Native ads (`NativeAdView`, `MediaView`, `INativeAdService`), see [native ads support](https://github.com/MartinZikmund/Uno.AdMob/issues).
- WebAssembly with AdSense, see [WebAssembly/AdSense (planned v2)](https://github.com/MartinZikmund/Uno.AdMob/issues).
- MacCatalyst. Windows and desktop compile, but show no ads.
