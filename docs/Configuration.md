## Ad unit IDs

Uno.AdMob allows you to explicitly specify an `AdUnitId` for each individual view or API.

For example, for a banner ad, you can specify the `AdUnitId` directly on the view:

```
<admob:BannerAd AdUnitId="ca-app-pub-xxxxxxxxxxxxxxxx/xxxxxxxxxx" />
```

While for an interstitial ad you can specify it when creating or preparing an ad:

```
IInterstitialAd CreateAd(string? adUnitId = null);
void PrepareAd(string? adUnitId = null);
```

## Default ad unit IDs

You can define default ad unit IDs in case you want to use the same IDs all throughout the application. You can configure them using [`AdConfig`](https://github.com/MartinZikmund/Uno.AdMob/blob/main/src/Uno.AdMob/Configuration/AdConfig.cs):

- `DefaultBannerAdUnitId` 
- `DefaultInterstitialAdUnitId`
- `DefaultRewardedAdUnitId`
- `DefaultRewardedInterstitialAdUnitId`
- `DefaultAppOpenAdUnitId`
- `DefaultNativeAdUnitId` (reserved, native ads are not supported yet)

> [!TIP]
> You can also pass default ad unit IDs directly to `services.AddAdMob(...)`. See [Setup](Setup.md).

## Test ad units

For testing purposes, you can set `AdConfig.UseTestAdUnitIds` to `true`. This will override any ad unit IDs you may have defined in your app, with [AdMob test IDs](https://github.com/MartinZikmund/Uno.AdMob/blob/main/src/Uno.AdMob/Configuration/AdMobTestAdUnits.cs) which display test ads and are safe to interact with.

## Test devices

When testing on real devices, you should register the device as a test device:

```csharp
using Uno.AdMob.Configuration;

AdConfig.AddTestDevice("<your-test-device-id>");
```

## Consent checks

By default the library will only request ads once consent requirements are satisfied.

You can opt out of consent checks by setting:
- `AdConfig.DisableConsentCheck = true`, or
- `services.AddAdMob(disableConsentCheck: true, ...)`

> [!NOTE]
> `disableConsentCheck` only skips the "can we request ads?" check before each ad request. It does **not** stop the consent form from showing: that is controlled by `automaticallyAskForConsent`. See [Consent](Consent.md#opting-out).

> [!IMPORTANT]
> Disabling consent checks makes compliance your responsibility.

## Targeting options (COPPA / EEA under age / content rating)

The following settings affect ad requests:

- `AdConfig.TagForChildDirectedTreatment` (`TagForChildDirectedTreatment`)
- `AdConfig.TagForUnderAgeOfConsent` (`TagForUnderAgeOfConsent`)
- `AdConfig.MaxAdContentRating` (`MaxAdContentRating`)

You can set them either via `AdConfig` or directly on `AddAdMob(...)`:

```csharp
using Uno.AdMob;
using Uno.AdMob.Configuration;

services.AddAdMob(
    tagForChildDirectedTreatment: TagForChildDirectedTreatment.None,
    tagForUnderAgeOfConsent: TagForUnderAgeOfConsent.None,
    maxAdContentRating: MaxAdContentRating.None);
```