## Troubleshooting (Consent / UMP / GDPR)

## Ads never load, but banners might

### Symptoms

- Ads don’t show, especially on a real device.
- You’re in a geography where consent may be required.

### Root cause

When consent is required, the library will not request ads until consent requirements are satisfied (unless you intentionally disabled consent checks).

### Fix / mitigation

- Ensure you configured consent dialogs in AdMob’s **Privacy & messaging** section.
- Use `IAdConsentService` to manage the consent flow if you opted out of automatic consent handling.
- During testing, force a geography and reset consent using debug settings (only works on test devices):

```csharp
services.AddAdMob();
services.AddConsentDebugSettings(new ConsentDebugSettings
{
    Geography = ConsentDebugGeography.Eea,
    TestDeviceHashedIds = ["<hashed-device-id>"],
    Reset = true
});
```

> [!IMPORTANT]
> You can disable consent checks via `services.AddAdMob(disableConsentCheck: true, ...)` or `AdConfig.DisableConsentCheck = true`, but compliance becomes your responsibility. This does not hide the consent form, use `automaticallyAskForConsent: false` for that.

### Related issues

- [`#5`](https://github.com/marius-bughiu/Plugin.AdMob/issues/5) / [`#9`](https://github.com/marius-bughiu/Plugin.AdMob/issues/9) (consent / GDPR / UMP requests)
