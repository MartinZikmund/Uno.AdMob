## Troubleshooting (Android)

## Build fails due to duplicate classes (measurement / compose / etc.)

### Symptoms

- `Type ... is defined multiple times`
- `JAVA0000: Compilation failed`
- Conflicts after adding another library (for example Firebase) to your app.

### Fix / mitigation

- **Clean aggressively**:
  - Clean the solution
  - Delete `bin` and `obj`
  - Rebuild
- **If you use Firebase and hit duplicate Google measurement classes** (see upstream issues #29 and #55, e.g. `Type com.google.android.gms.internal.measurement.zzbm is defined multiple times`): the Google "measurement" classes ship in both `Xamarin.GooglePlayServices.Measurement.*` and `Xamarin.Firebase.Analytics`. The build only works when the whole family resolves to the same Google release train. Align them by referencing `Xamarin.Firebase.Analytics` directly in your app at the version matching the resolved `Xamarin.GooglePlayServices.Measurement.Base` (check `obj/project.assets.json`). Uno.AdMob uses the Next-Gen Google Mobile Ads SDK, which doesn't depend on `Xamarin.GooglePlayServices.Measurement.*`, so this should only come up if another package brings them in.

  Note: enabling MultiDex or switching to d8/r8 does **not** fix this, d8/r8 fail on duplicate type definitions rather than deduplicating them.

### Related issues

- [`#29`](https://github.com/marius-bughiu/Plugin.AdMob/issues/29) (duplicate measurement classes when combined with Firebase)
- [`#67`](https://github.com/marius-bughiu/Plugin.AdMob/issues/67) / [`#80`](https://github.com/marius-bughiu/Plugin.AdMob/issues/80) (duplicate Compose annotation classes, fixed upstream by the Ads SDK dependency update)

## AndroidManifest namespace warnings on .NET 10 / API 35+

### Symptoms

- Warnings like:
  - `Namespace 'com.google.android.gms.ads' is used in multiple modules and/or libraries`
- A build error like `uses-sdk:minSdkVersion 23 cannot be smaller than version 24 declared in library`.

### Fix / mitigation

Make sure your app explicitly sets a minimum Android platform version compatible with the Google Mobile Ads Next-Gen SDK. Set `SupportedOSPlatformVersion` to 24 (the SDK depends on `androidx.webkit`, which requires API 24):

```xml
<PropertyGroup Condition="'$(TargetFramework)' == 'net10.0-android'">
	<SupportedOSPlatformVersion>24.0</SupportedOSPlatformVersion>
</PropertyGroup>
```

If your project sets the value in more than one place, MSBuild uses the last one it sees.

### Related issues

- [`#68`](https://github.com/marius-bughiu/Plugin.AdMob/issues/68) (namespace warnings when min platform is too low)

## NU1608 warnings: "Detected package version outside of dependency constraint"

### Symptoms

Building an app that references the library emits warnings like:

```
warning NU1608: Detected package version outside of dependency constraint: Xamarin.AndroidX.Fragment.Ktx 1.8.8.1 requires Xamarin.AndroidX.Fragment (>= 1.8.8.1 && < 1.8.9) but version Xamarin.AndroidX.Fragment 1.8.9.1 was resolved.
```

### Root cause

The Google Mobile Ads SDK's dependencies declare upper bounds on AndroidX packages that don't admit the newer versions your app resolves. The pairs are compatible in practice, NU1608 is a warning, not an error.

### Fix / mitigation

Reference the matching `Ktx` packages directly in your app project at versions aligned with the resolved base packages (look at the warnings themselves or `obj/project.assets.json`). Uno.AdMob itself requires `Xamarin.AndroidX.Fragment.Ktx` 1.9.1 or newer, so an older reference in your app fails restore (NU1107 with 1.8.x, NU1605 with 1.9.0).

### Related issues

- [`#82`](https://github.com/marius-bughiu/Plugin.AdMob/issues/82) (NU1608 dependency constraint warnings)

## Full-screen test ads fail with "no fill" on emulators (banners work)

### Symptoms

- Interstitial, rewarded, rewarded interstitial, and app open test ads fail to load with `Ad failed to load : 3` ("No fill.") on an Android emulator, while banner test ads load and render fine.
- Consent is granted and `AdConfig.UseTestAdUnitIds = true`, so a fill is expected every time.
- The full `LoadAdError` response shows the demo campaign *was* served, but the adapter failed instantly:

```
"Ad Source Instance Name": "[DO NOT EDIT] Publisher Test Interstitial",
"Ad Error": { "Code": 0, "Message": "Internal error.", ... },
"Latency": 0
```

### Root cause

The emulator is running with software GPU rendering (`-gpu swiftshader_indirect`, or "Graphics: Software" in the AVD settings). Full-screen ad creatives are pre-rendered at load time and fail with an internal error without hardware acceleration. The SDK surfaces this as a generic no-fill. Banner ads are not affected, which makes it look like an ad-serving or library problem when it is not.

### Fix / mitigation

- Start the emulator with hardware graphics acceleration: `emulator -avd <name> -gpu host`, or set **Graphics: Hardware** in the AVD settings in Android Studio.
- To see the underlying error instead of a bare "no fill", subscribe to `OnAdFailedToLoad` on the ad or ad service and log the message.
- Physical devices are unaffected.

## The app starts and instantly exits ("No assemblies found ... Fast Deployment")

### Symptoms

- You built the Android head without running it from your IDE and installed the APK by hand (`adb install`). The app closes right away, and logcat says `No assemblies found in '.../.__override__/...'. Assuming this is part of Fast Deployment`.

### Fix / mitigation

Debug builds don't embed the assemblies in the APK. Build with `-p:EmbedAssembliesIntoApk=true`, or run it with `dotnet build -t:Run`.

## Runtime errors on older Android versions

### Symptoms

- `Java.Lang.NoSuchMethodError` mentioning `getDisplay()`
- Analyzer warnings like `'Context.Display' is only supported on: 'android' 30.0 and later.`

### Fix / mitigation

- Update to the latest Uno.AdMob version.
- If you still reproduce it, capture the exception + target Android version and report it.

### Related issues

- [`#40`](https://github.com/marius-bughiu/Plugin.AdMob/issues/40) (older Android compatibility / `getDisplay()` errors)

## Testing the packed package

R8 and the NuGet package layout only get exercised when the sample consumes the real nupkg instead of the project. The sample has a switch for that:

```bash
dotnet pack src/Uno.AdMob/Uno.AdMob.csproj -c Release -o ./feed

dotnet build samples/Uno.AdMob.Sample/Uno.AdMob.Sample/Uno.AdMob.Sample.csproj   -f net10.0-android -c Release   -p:UseAdMobPackage=true -p:AdMobPackageVersion=<version of the nupkg in ./feed>   -p:RestoreAdditionalProjectSources=$PWD/feed   -p:AndroidLinkTool=r8 -p:AndroidEnableProguard=true
```

`AndroidLinkTool=r8` turns R8 on (Release doesn't run it by default). The build should succeed with no "defined multiple times" errors. Afterwards, delete the `mzikmund.uno.admob` folder from your NuGet global packages folder so the local build doesn't shadow the real feed.
