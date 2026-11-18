# Uno.AdMob

Google AdMob ads for [Uno Platform](https://platform.uno/) apps on Android and iOS: banner, interstitial, rewarded, rewarded interstitial and app open ads, plus GDPR consent with Google's User Messaging Platform (UMP).

This library is a port of [Plugin.AdMob](https://github.com/marius-bughiu/Plugin.AdMob) by Marius Bughiu, who did the hard part. The API mirrors his on purpose, so his docs and issue tracker are useful here too. Thank you, Marius!

```bash
dotnet add package MZikmund.Uno.AdMob
```

## Platform Support

| Platform | Banner | Interstitial | Rewarded | Rewarded interstitial | App open | Consent (UMP) |
|---|---|---|---|---|---|---|
| Android (API 24+) | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| iOS (15.0+) | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| Windows / desktop / WebAssembly | compiles, no ads | compiles, no ads | compiles, no ads | compiles, no ads | compiles, no ads | compiles, no ads |

The package targets `net10.0-android`, `net10.0-ios`, `net10.0-windows10.0.19041` and `net10.0`, and is built on Uno Platform 7.0 (`Uno.Sdk`).

**Not yet supported:** [native ads support](https://github.com/MartinZikmund/Uno.AdMob/issues) and [WebAssembly/AdSense (planned v2)](https://github.com/MartinZikmund/Uno.AdMob/issues).

## Setup

### 1. Platform configuration

**Android**: in `Platforms/Android/AndroidManifest.xml`, add the AdMob app ID and the permissions:

```xml
<manifest ...>
  <application ...>
    <meta-data
        android:name="com.google.android.gms.ads.APPLICATION_ID"
        android:value="ca-app-pub-3940256099942544~3347511713" />
  </application>
  <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
  <uses-permission android:name="android.permission.INTERNET" />
</manifest>
```

The ID above is Google's sample ID. Replace it with yours when you go live. The minimum Android version must be 24.

**iOS**: in `Platforms/iOS/Info.plist`, add your app ID, `GADIsAdManagerApp` and the `SKAdNetworkItems` from Google's [quick start](https://developers.google.com/admob/ios/quick-start):

```xml
<key>GADApplicationIdentifier</key>
<string>ca-app-pub-3940256099942544~1458002511</string>
<key>GADIsAdManagerApp</key>
<true/>
```

The minimum iOS version must be 15.0. See the [Setup guide](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Setup.md) for the full list.

### 2. Register and initialize

Register the services, then hand the built service provider to `AdMobPlatform`:

```csharp
using Uno.AdMob;
using Uno.AdMob.Configuration;

// In your host configuration
services.AddAdMob();
AdConfig.UseTestAdUnitIds = true; // Google's test ads, safe to tap

// After building the host
AdMobPlatform.Initialize(Host.Services);
```

No hosting? `new ServiceCollection().AddAdMob().BuildServiceProvider()` works as well.

`AddAdMob` takes the same parameters as upstream's `UseAdMob`: default ad unit IDs per platform, `automaticallyAskForConsent`, `disableConsentCheck` and the targeting options (`tagForChildDirectedTreatment`, `tagForUnderAgeOfConsent`, `maxAdContentRating`).

The library starts the Google Mobile Ads SDK for you once consent allows ad requests.

## Usage

### Banner ads

```xml
<Page xmlns:admob="using:Uno.AdMob" ...>
    <admob:BannerAd AdUnitId="ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx" />
</Page>
```

`AdUnitId` is optional with test ads. Other members: `AdSize`, `CustomAdWidth`, `CustomAdHeight`, `IsLoaded` and the events `OnAdLoaded`, `OnAdFailedToLoad`, `OnAdImpression`, `OnAdClicked`, `OnAdSwiped`, `OnAdOpened`, `OnAdClosed`. See [Banner ads](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Banner-ads.md).

### Interstitial ads

```csharp
var service = AdMobPlatform.Services!.GetRequiredService<IInterstitialAdService>();

service.OnAdLoaded += (s, e) => service.ShowAd();
service.PrepareAd(); // or PrepareAd("your-ad-unit-id")
```

Or inject `IInterstitialAdService` wherever you like. Need per-ad events (`OnAdShowed`, `OnAdDismissed`...)? Use `CreateAd()` and subscribe to the returned `IInterstitialAd`.

### Rewarded ads

```csharp
var service = AdMobPlatform.Services!.GetRequiredService<IRewardedAdService>();

service.OnAdLoaded += (s, e) => service.ShowAd();
service.PrepareAd(onUserEarnedReward: reward => Console.WriteLine($"{reward.Amount} {reward.Type}"));
```

### Rewarded interstitial ads

Same shape as rewarded ads, with `IRewardedInterstitialAdService`.

### App open ads

Same shape as interstitial ads, with `IAppOpenAdService`.

Each format has its own page in the [docs](#documentation), with the full interface and the per-ad API.

## Consent

When consent is required (for example in the EEA), the UMP form is shown automatically on startup, and ads are only requested once consent allows it. You can take control through `IAdConsentService` (`LoadAndShowConsentFormIfRequired`, `CanRequestAds`, `IsPrivacyOptionsRequired`, `ShowPrivacyOptionsForm`, `Reset`).

Two switches that are easy to mix up:

- `automaticallyAskForConsent: false` stops the form from showing on startup.
- `disableConsentCheck: true` only skips the "can we request ads?" gate before each ad request. The form still shows.

To see the form while developing, force a geography (works on test devices and emulators):

```csharp
services.AddConsentDebugSettings(new ConsentDebugSettings
{
    Geography = ConsentDebugGeography.Eea,
    Reset = true,
});
```

See [Consent](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Consent.md), including the iOS ATT notes.

## Test ads

Set `AdConfig.UseTestAdUnitIds = true` and every ad unit ID is swapped for one of Google's test IDs. On real devices, also register the device:

```csharp
AdConfig.AddTestDevice("<your-test-device-id>");
```

Never tap real ads on your own device, Google really dislikes that.

## Unsupported platforms

On Windows, desktop and WebAssembly the package compiles and runs, so your shared code needs no `#if`. Nothing is shown though:

- `AdMobPlatform.IsSupported` is `false`.
- `BannerAd` stays empty and never raises `OnAdLoaded`.
- Loading an interstitial, rewarded, rewarded interstitial or app open ad raises `OnAdFailedToLoad` ("Ads are only supported on Android and iOS.").
- `IAdConsentService` has nothing to show, and **`IMobileAdsService.Initialization` never completes**. If your code awaits it, give it a timeout, for example `Initialization.WaitAsync(TimeSpan.FromSeconds(60))`, or check `AdMobPlatform.IsSupported` first.

## Documentation

1. [Setup](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Setup.md): initialization, Android manifest and iOS `Info.plist`
1. [Configuration](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Configuration.md): default ad unit IDs, test ads, test devices
1. [Consent](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Consent.md): GDPR consent with UMP, debug geographies
1. [Banner ads](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Banner-ads.md)
1. [Interstitial ads](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Interstitial-ads.md)
1. [Rewarded ads](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Rewarded-ads.md)
1. [Rewarded interstitial ads](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Rewarded-interstitial-ads.md)
1. [App open ads](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/App-open-ads.md)
1. [Upgrading](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Upgrading.md), including moving from Plugin.AdMob
1. [Troubleshooting](https://github.com/MartinZikmund/Uno.AdMob/blob/main/docs/Troubleshooting.md)

A working app lives in [samples/Uno.AdMob.Sample](https://github.com/MartinZikmund/Uno.AdMob/tree/main/samples/Uno.AdMob.Sample): one button per ad format, an event log and the consent controls.

## Staying in sync with upstream

Uno.AdMob mirrors Plugin.AdMob file by file, so upstream improvements can be ported with a plain diff. The synced version and the file-by-file map are in [UPSTREAM.md](https://github.com/MartinZikmund/Uno.AdMob/blob/main/UPSTREAM.md#how-to-sync) ("Staying in sync").

## License

MIT. AdMob is a trademark of Google LLC; this project has no affiliation with Google.
