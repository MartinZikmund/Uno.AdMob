## Troubleshooting

This section is based on common issues seen in real projects, and on closed issues from the upstream [Plugin.AdMob](https://github.com/marius-bughiu/Plugin.AdMob) repository. Issue numbers (`#12`) refer to that repository.

## Choose a topic

- **iOS setup & crashes**: [Troubleshooting-iOS](Troubleshooting-iOS.md)
- **Android build / dependency issues**: [Troubleshooting-Android](Troubleshooting-Android.md)
- **Consent / UMP / GDPR**: [Troubleshooting-Consent](Troubleshooting-Consent.md)
- **Ads not showing (general)**: [Troubleshooting-Ads-not-showing](Troubleshooting-Ads-not-showing.md)
- **Banner ads**: [Troubleshooting-Banner-ads](Troubleshooting-Banner-ads.md)
- **Interstitial ads**: [Troubleshooting-Interstitial-ads](Troubleshooting-Interstitial-ads.md)

## Quick checklist

- **Android**: ensure `com.google.android.gms.ads.APPLICATION_ID` is set in `Platforms/Android/AndroidManifest.xml` and you have `INTERNET` + `ACCESS_NETWORK_STATE` permissions.
- **iOS**: ensure `GADApplicationIdentifier` is set in `Platforms/iOS/Info.plist` as described in [Setup](Setup.md). The library starts the SDK once consent allows ad requests.
- **Initialization**: call `AdMobPlatform.Initialize(Host.Services)` after building your host. Without it, ad services throw "Uno.AdMob is not initialized".
- **Consent**: if consent is required, ads will not be requested until consent is obtained (unless you intentionally disabled consent checks).
- **Desktop / Windows / WebAssembly**: no ads are shown there by design, check `AdMobPlatform.IsSupported`.
- **New apps**: newly created / re-activated AdMob apps can take time before real ads have inventory.
