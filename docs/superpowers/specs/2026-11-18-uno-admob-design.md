# Uno.AdMob – design

Date: 2026-11-18 · Status: draft for approval

## Intent

Port [marius-bughiu/Plugin.AdMob](https://github.com/marius-bughiu/Plugin.AdMob) (Google AdMob for .NET MAUI) to **Uno Platform 7.0**, so Martin's Uno apps can show AdMob ads with the same API MAUI developers already know. Shipped as a public open-source NuGet package under his brand, built and released exactly like his existing [Uno.RevenueCat](https://github.com/MartinZikmund/Uno.RevenueCat) library.

The overriding non-functional goal is **easy updating**: pulling in new upstream Plugin.AdMob releases, new Google SDK bindings and new Uno.Sdk versions must be cheap and mechanical.

## Goals (v1)

- Formats: **banner, interstitial, rewarded, rewarded interstitial, app open**, plus **UMP consent** (GDPR form, privacy options) and mobile-ads initialization / request configuration (test devices, child-directed tags, max content rating, test ad unit IDs).
- Platforms with real ads: **Android and iOS** (Uno.Sdk, Skia renderer, which Martin's apps use).
- A **plain `net10.0` fallback** so shared code and XAML compile and run on every other Uno head (desktop, WASM, Windows): controls take part in layout at 0×0, services resolve and report "unsupported" through their normal failure events, and a runtime `IsSupported` check exists.
- Public API names and shapes kept identical to upstream (`IInterstitialAdService`, `IRewardedAdService`, `IAppOpenAdService`, `IAdConsentService`, `IMobileAdsService`, `BannerAd`, `AdConfig`, `AdSize`, events like `OnAdLoaded` / `OnAdFailedToLoad`).

## Non-goals (v1)

- **Native ads** (`NativeAdView`, `MediaView`, `INativeAd`, `INativeAdService`, `VideoOptions`). On Skia, the XAML template is painted on the canvas and cannot live inside the native `NativeAdView`; native embedding hosts only a single leaf native view. A plausible design (transparent native `NativeAdView` overlay + manually positioned native `MediaView` over a XAML placeholder) exists but needs investigation. Tracked as a GitHub issue, not shipped half-done.
- **WebAssembly via AdSense** – planned for v2. v1 only has to keep the public API platform-neutral so a WASM implementation can slot into the `net10.0`/browser path later without breaking changes.
- MacCatalyst and Windows-native implementations (the fallback covers them), the Blazor Hybrid sample and docs.

## Anti-goals

- Diverging from upstream structure "because it's cleaner". Every gratuitous rename or restructure makes the next upstream port more expensive.
- Shipping a partial native-ads API.
- Throwing on unsupported platforms. Apps must not need `#if` or try/catch to stay cross-platform.

## Constraints and facts

- Upstream sync point: **tag `v11.0.0.0`**, commit `2e1bc29d84bd11290fec314dd57f89b665064ef5` (MAUI 11 / net11). `main` already has later commits; they get ported in a later sync.
- Uno 7.0 is on .NET 10, so the package targets `net10.0-*` even though upstream is on `net11.0-*`. All bindings have net10 assets:
  - Android: `Xamarin.Google.Android.Libraries.Ads.Mobile.Sdk` 1.5.0 (Google Next-Gen Mobile Ads SDK, includes UMP), plus `Xamarin.AndroidX.Fragment.Ktx` 1.9.1 as upstream does (it fixes a duplicate-class R8 error).
  - iOS: `Jc.GMA.iOS` 12.10.0 and `Jc.UMP.iOS` 2.7.5 – **the exact versions upstream v11.0.0.0 pins**, so the ported code compiles against the API it was written for. Moving to GMA 13.x / UMP 3.x is a separate follow-up, done when upstream does it or deliberately.
- Minimum OS: Android API 24 (the SDK depends on androidx.webkit), iOS 15.0 (upstream's floor).
- License: upstream is MIT. Our LICENSE keeps Marius Bughiu's copyright notice alongside Martin's; the README credits upstream like Uno.RevenueCat credits Kebechet.

## Naming

| Thing | Value |
|---|---|
| GitHub repo | `MartinZikmund/Uno.AdMob` (**public**) |
| Local folder | `D:\Personal\Uno.AdMob` |
| NuGet package | `MZikmund.Uno.AdMob` |
| Root namespace | `Uno.AdMob` (upstream `Plugin.AdMob` → `Uno.AdMob`; sub-namespaces `.Configuration`, `.Services`, `.Handlers`, `.Platforms.*` kept) |

## Approaches considered

1. **Mirror port (chosen).** Copy upstream source at the sync tag into the same relative paths, rename only the root namespace, and replace only MAUI-specific code. Mark each Uno-specific divergence with a short `// Uno:` comment. Upstream diffs then apply file-by-file. This costs some code that looks MAUI-shaped (for example internal `*Handler` classes), which is a fair price for updatability.
2. Clean-room Uno-idiomatic rewrite (one service class per format, XAML-first API). Nicer code, but every upstream release would need manual re-reading and re-implementing. Rejected: it works against the main goal.
3. Wrap the MAUI package via Uno's MAUI embedding. This drags MAUI into every Uno app and doesn't fit Uno's Skia renderer. Rejected.

Picked by Martin (layout mirroring + fast dependency bumps).

## Design

### Repository layout (mirrors upstream + Uno.RevenueCat build infra)

```
Uno.AdMob/
  .github/workflows/package.yml     # copy of Uno.RevenueCat's (build, test, NuGet Trusted Publishing)
  .github/workflows/upstream-watch.yml  # optional, see "Left to the builder"
  .github/dependabot.yml            # nuget (Directory.Packages.props) + github-actions
  docs/                             # upstream docs adapted to Uno (Setup, Banner, Interstitial, Rewarded, Rewarded-interstitial, App-open, Consent, Configuration, Troubleshooting*)
  samples/Uno.AdMob.Sample/         # Uno single-project app: android, ios, desktop heads
  src/
    Directory.Build.props / .targets / Directory.Packages.props  # as in Uno.RevenueCat (CPM, NBGV, nullable, author)
    global.json                     # Uno.Sdk 7.0.x (latest stable 7.0 at build time; 7.0.22 today), .NET SDK 10.0.4xx
    version.json                    # NBGV, same scheme as Uno.RevenueCat
    Uno.AdMob.slnx
    Uno.AdMob/                      # same relative file paths as upstream src/Plugin.AdMob/
    Uno.AdMob.Tests/                # MSTest, net10.0
  UPSTREAM.md                       # sync record, see below
  README.md, LICENSE
```

### Project file

- `Sdk="Uno.Sdk"`, `TargetFrameworks` = `net10.0-android;net10.0-ios;net10.0-windows10.0.19041;net10.0` (see builder-check decisions).
- Package metadata like Uno.RevenueCat (`PackageId` `MZikmund.Uno.AdMob`, icon, README, LICENSE, tags, embedded symbols, `GenerateDocumentationFile`).
- Versioning via Nerdbank.GitVersioning (not upstream's MinVer + MAUI-tracked scheme). `version.json` starts at `1.0-beta.{height}`, prereleases from `main`, stable from `release/x.y` – Claude's call, easy to change.
- Platform code filtering: exclude `Platforms/Android/**` outside android, `Platforms/iOS/**` outside ios. Upstream's `Platforms/MacCatalyst/**` and `Platforms/Windows/**` are not copied, because the `net10.0` fallback replaces them.
- Upstream's `buildTransitive/Plugin.AdMob.targets` + `.proguard.cfg` are carried over (renamed to `MZikmund.Uno.AdMob.*` so NuGet picks them up) – the ProGuard rules are needed for R8 on Android.

### Mapping MAUI to Uno

| Upstream (MAUI) | Uno.AdMob |
|---|---|
| `Config.UseAdMob(this MauiAppBuilder, …same params…)` | `AdMobExtensions.AddAdMob(this IServiceCollection, …same params…)`. Same parameter list and order, same DI registrations, same `AdConfig` side effects. `UseConsentDebugSettings` → `AddConsentDebugSettings(this IServiceCollection, …)`. |
| `IPlatformApplication.Current.Services` (used by handlers to resolve services) | New static `AdMob.Initialize(IServiceProvider)` stores the provider; controls resolve from it. Using a control before `Initialize` throws a clear `InvalidOperationException` on Android/iOS. Apps without DI build a provider with `new ServiceCollection().AddAdMob(...).BuildServiceProvider()` (documented). |
| MAUI lifecycle events (`android.OnStart`, `ios.OnActivated` / `SceneOnActivated`) that trigger the automatic consent flow | Hooked natively inside `AdMob.Initialize`: Android `Application.IActivityLifecycleCallbacks.OnActivityStarted`, iOS `UIApplication.DidBecomeActiveNotification`. Same "run once, reset on failure" logic as upstream's `OnStart()`. If the app is already active when `Initialize` runs, the flow starts immediately. |
| `MainThread.IsMainThread` / `BeginInvokeOnMainThread` (Android `MainThreadDispatcher`) | Native main-looper check / `Handler(Looper.MainLooper).Post`. On iOS, `NSThread.IsMain` / `BeginInvokeOnMainThread`. No UI-framework dependency. |
| `Platform.CurrentActivity` | Activity resolver modelled on Uno.RevenueCat's `ActivityResolver` (lifecycle-tracked current activity, fallback `Uno.UI.ContextHelper.Current`). |
| iOS root view controller lookup | Upstream code kept as is if it uses UIKit directly; otherwise key window → top-most presented controller. |
| `BannerAd : ContentView` + `BannerAdHandler : ViewHandler<BannerAd, AdView/BannerView>` | `BannerAd : ContentControl` with DependencyProperties mirroring the BindableProperties (`AdUnitId`, `AdSize`, `CustomAdWidth`, `CustomAdHeight`, read-only `IsLoaded`) and the same events. The internal `BannerAdHandler` class and file keep the upstream name and body; only its base class and connect/disconnect wiring change. `BannerAd` creates it on `Loaded` and drops it on `Unloaded`, and the handler's native ad view is set as `Content` (Uno native-view embedding on Skia). Size requests upstream applies to the MAUI view become `Width`/`Height` on the `BannerAd`. |
| `Application.Current.Dispatcher.Dispatch` in `UnsupportedPlatform.PostLoadFailure` | `DispatcherQueue.GetForCurrentThread()?.TryEnqueue(raise)`, otherwise invoke directly. **The only change to that file.** |

### Unsupported platforms (`net10.0` fallback)

Upstream already ships this behaviour for MacCatalyst/Windows in `UnsupportedPlatform.cs` (`#if !ANDROID && !IOS`): `Load()` raises `OnAdFailedToLoad` with `AdError.UnsupportedPlatformMessage`, `Show()` is a no-op, consent `CanRequestAds()` is `false`, and `MobileAdsService` has its own `#else` branch. Uno.AdMob **reuses those upstream files and `#if` guards unchanged** (except the dispatcher line above), so upstream fixes to the fallback port mechanically. Uno-specific additions are kept to:

- `AdMob.IsSupported` – `true` on android/ios builds, `false` otherwise (compile-time constant behind a static property).
- `BannerAd` on fallback: no handler, empty `Content`, measures 0×0, `IsLoaded` stays `false`, and it raises `OnAdFailedToLoad` with the unsupported message once when loaded into the tree (consistent with the services). This replaces upstream's Windows `BannerAdHandler` (an empty `Border`).
- Native-ad stubs in `UnsupportedPlatform.cs` are removed together with the rest of native ads (an `// Uno:` note marks it).

### Updating (the core requirement)

- **`UPSTREAM.md`** records the synced tag and commit, plus a table of every upstream file with status `ported` / `adapted (Uno)` / `skipped (reason)` and its Uno path. Porting a new upstream release = `git diff <old-tag> <new-tag> -- src/Plugin.AdMob` and walk the table.
- File paths, type names, member names and `#if` structure match upstream. Uno-specific changes are marked with a short `// Uno:` comment, so they stand out in a diff.
- All package versions are in `Directory.Packages.props` (CPM). Uno.Sdk is in `global.json`. Dependabot raises PRs for NuGet packages and GitHub Actions; CI builds every PR, so a binding bump that breaks the API shows up immediately.

### Testing

- Approach: TDD where it makes sense. Platform ad code can't be unit-tested without devices.
- `Uno.AdMob.Tests` (MSTest, `net10.0`, referencing the fallback build) covers:
  - `AddAdMob` registrations and `AdConfig` side effects (defaults, test unit IDs, tags, content rating)
  - fallback behaviour: `Load` raises `OnAdFailedToLoad` with the unsupported message, `IsLoaded` stays false, consent `CanRequestAds` is false, `IsSupported` is false
  - `ConsentGate` logic
  - test-ad-unit resolution
- `samples/Uno.AdMob.Sample` uses Google's test ad unit IDs and has a button per format plus a banner, and a consent / privacy-options page. Martin verifies it on a real Android device and an iOS device/simulator.
- CI (windows-latest, like Uno.RevenueCat) builds the library for all three TFMs, runs tests and packs. The sample's iOS head isn't built in CI (needs a Mac).

### GitHub

- Create a public repo `MartinZikmund/Uno.AdMob` with a description and topics, then push.
- Open issues: "Native ads support (Skia overlay investigation)", with the overlay design notes from the brainstorm, and "v2: WebAssembly support via AdSense".
- NuGet Trusted Publishing: same workflow as Uno.RevenueCat (`NuGet/login`, user `mzikmunddev`). **Martin must add a trusted-publishing policy for the new repo on nuget.org** before the first publish works.

## Decisions from the builder check

- **Windows head** *(Claude's call)*: a plain `net10.0` asset compiled against Uno.WinUI isn't usable from a WinAppSDK head. Add a fourth TFM, `net10.0-windows10.0.19041`, which compiles the same fallback code (no native bindings). Final TFMs: `net10.0-android;net10.0-ios;net10.0-windows10.0.19041;net10.0`. CI builds all four.
- **Native-ad config surface** *(Claude's call, in favour of portability)*: the `AddAdMob` parameter list stays **identical** to upstream, including `androidDefaultNativeAdUnitId` / `iosDefaultNativeAdUnitId`. `AdConfig.DefaultNativeAdUnitId` and the native entries in the test-unit tables also stay. They are plain configuration data that the future native-ads work will use, and keeping them keeps the signature and diffs aligned. Removed: `NativeAd`, `NativeAdView`, `MediaView`, `INativeAd`, `INativeAdService`, `NativeAdService`, `VideoOptions`, native handlers and their DI/handler registrations. XML docs on the kept native parameters say "reserved – native ads are not supported yet".
- **Banner embedding risk** *(Claude's call)*: Uno's docs state that native views set as `ContentControl.Content` are hosted on Skia Android/iOS with layout, clipping and z-order synced. The plan still **front-loads a banner spike** (sample app, test unit, Android first). If embedding or sizing misbehaves, stop and report to Martin rather than invent another architecture. Uno's native (non-Skia) renderer is best-effort, not a v1 requirement.
- **Where v1 ends** *(Martin's answer)*: CI is the same as Uno.RevenueCat, so pushes to `main` publish a NuGet prerelease. Before the first push, Martin adds a nuget.org Trusted Publishing policy for `MartinZikmund/Uno.AdMob`. v1 is done when the public repo is pushed, CI is green and `MZikmund.Uno.AdMob 1.0.0-beta.N` is on nuget.org. Stable `1.0` comes later via `release/1.0`, after device testing.
- **Upstream `tests/` and extra workflows** *(Claude's call)*: upstream's DeviceTests, PackageConsumer, `binding-drift`, `bump-maui`, `nightly`, `device-tests`, `sync-wiki` and `release` workflows are MAUI-/repo-specific. They are recorded in `UPSTREAM.md` as `skipped`, with a reason; candidates to adapt later are noted there.

## Left to the builder

- Whether to add an `upstream-watch.yml` scheduled workflow that opens an issue when Plugin.AdMob publishes a new release (cheap; recommended but not required).
- Exact handling when the iOS/Android bindings expose slightly different API surface on net10 vs net11. Adapt minimally and mark with `// Uno:`.
- Sample app UI details.
- Exact .NET SDK feature band in `global.json` (match what is installed locally and on CI).
