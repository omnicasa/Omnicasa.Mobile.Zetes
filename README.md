# Omnicasa.Mobile.Zetes

A .NET MAUI library for integrating Zetes eID SDK to read Belgian eID cards on iOS and Android platforms.

## Overview

This library provides a cross-platform .NET MAUI wrapper around the native Zetes eID SDK, enabling you to read Belgian electronic identity cards (eID) from your .NET mobile applications. The library uses Reactive Extensions (Rx.NET) for event-driven programming and provides a clean, platform-agnostic API.

## Features

- **Cross-platform support**: iOS and Android
- **Reactive API**: Built on System.Reactive for event-driven programming
- **Type-safe**: Strongly-typed interfaces and models
- **Error handling**: Comprehensive exception handling for various error scenarios
- **Logging**: Built-in logging support via observables
- **Reader support**: Supports BLE (Bluetooth Low Energy) and wired readers (iR301, BR301)

## Requirements

### Development Environment

- .NET SDK 8.0.415 or later
- .NET MAUI workload installed
- Visual Studio 2022 or JetBrains Rider (recommended)
- Xcode (for iOS development)
- Android SDK (for Android development)

### Runtime Requirements

- **iOS**: iOS 13.0 or later
- **Android**: Android API 26 (Android 8.0) or later
- Compatible Zetes eID card reader (BLE or wired)

## Installation

### Install .NET MAUI Workload

```bash
dotnet workload install maui
```

### NuGet Packages

The solution consists of multiple NuGet packages:

1. **[Omnicasa.Mobile.Zetes.Standard](https://www.nuget.org/packages/Omnicasa.Mobile.Zetes.Standard/)** - Core interfaces and models (.NET Standard 2.0)
2. **[Omnicasa.Mobile.Zetes](https://www.nuget.org/packages/Omnicasa.Mobile.Zetes/)** - Main MAUI library
3. **[Omnicasa.Mobile.Zetes.iOS](https://www.nuget.org/packages/Omnicasa.Mobile.Zetes.iOS/)** - iOS native bindings
4. **[Omnicasa.Mobile.Zetes.Droid](https://www.nuget.org/packages/Omnicasa.Mobile.Zetes.Droid/)** - Android native bindings

Add the main package to your .NET MAUI project:

```xml
<PackageReference Include="Omnicasa.Mobile.Zetes" Version="0.0.0.1" />
```

Or install via Package Manager Console:

```powershell
Install-Package Omnicasa.Mobile.Zetes
```

Or via .NET CLI:

```bash
dotnet add package Omnicasa.Mobile.Zetes
```

The platform-specific packages will be automatically included as dependencies.

## Usage

### Basic Example

```csharp
using System.Reactive.Linq;
using Omnicasa.Mobile.Zetes.Standard;
using Omnicasa.Mobile.Zetes.iOS; // or .Droid for Android

// Create service instance
IZetesService zetesService = new ZetesService();

// Subscribe to logs
zetesService.Logs()
    .Subscribe(log => Console.WriteLine($"Log: {log}"));

// Subscribe to scanning events
zetesService.Scanning()
    .Skip(1) // Skip initial null value
    .Subscribe(@event =>
    {
        switch (@event.State)
        {
            case ZetesState.CardDetected:
                var cardInfo = @event.CardInfo;
                Console.WriteLine($"Name: {cardInfo.FirstName} {cardInfo.LastName}");
                Console.WriteLine($"Card Number: {cardInfo.CardNumber}");
                break;
            case ZetesState.Error:
                Console.WriteLine($"Error: {@event.Exception?.Message}");
                break;
        }
    });

// Start scanning for readers
zetesService.StartScan().Subscribe();

// Stop scanning when done
zetesService.StopScan().Subscribe();
```

### Complete Example

```csharp
using System.Reactive.Linq;
using Omnicasa.Mobile.Zetes.Standard;
using Omnicasa.Mobile.Zetes.iOS;

public class MainPage : ContentPage
{
    private IZetesService zetesService;

    public MainPage()
    {
        InitializeComponent();
        
        #if __IOS__
        zetesService = new Omnicasa.Mobile.Zetes.iOS.ZetesService();
        #else
        zetesService = new Omnicasa.Mobile.Zetes.Droid.ZetesService();
        #endif

        // Subscribe to logs
        zetesService.Logs()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(log => 
            {
                Console.WriteLine($"Zetes: {log}");
                // Update UI with log message
            });

        // Subscribe to scanning events
        zetesService.Scanning()
            .Skip(1)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(@event =>
            {
                switch (@event.State)
                {
                    case ZetesState.Starting:
                        // Show scanning indicator
                        break;
                    case ZetesState.CardDetected:
                        var card = @event.CardInfo;
                        // Display card information
                        DisplayCardInfo(card);
                        break;
                    case ZetesState.Error:
                        // Handle error
                        DisplayError(@event.Exception);
                        break;
                    case ZetesState.Stopped:
                        // Scanning stopped
                        break;
                }
            });
    }

    private void StartScanButton_Clicked(object sender, EventArgs e)
    {
        zetesService.StartScan().Subscribe();
    }

    private void StopScanButton_Clicked(object sender, EventArgs e)
    {
        zetesService.StopScan().Subscribe();
    }

    private void DisplayCardInfo(EidCardInfo card)
    {
        // Display card information in UI
    }

    private void DisplayError(Exception ex)
    {
        // Display error message
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        zetesService?.Dispose();
    }
}
```

## API Reference

### IZetesService

The main service interface for interacting with Zetes eID readers.

#### Methods

- `IObservable<ZetesEvent> StartScan()` - Starts scanning for eID card readers
- `IObservable<ZetesEvent> StopScan()` - Stops scanning for readers
- `IObservable<ZetesEvent> Scanning()` - Observable stream of scanning events
- `IObservable<bool> IsSupported()` - Checks if eID reading is supported on the device
- `IObservable<string> Logs()` - Observable stream of log messages

### ZetesEvent

Represents an event from the Zetes service.

```csharp
public class ZetesEvent
{
    public EidCardInfo CardInfo { get; set; }
    public Exception Exception { get; set; }
    public ZetesState State { get; set; }
}
```

### ZetesState

Enumeration of possible states:

- `Unknown` - Initial or unknown state
- `Stopped` - Scanning has stopped
- `Starting` - Scanning is starting
- `CardDetected` - A card has been detected and read
- `Error` - An error occurred

### EidCardInfo

Contains all information read from the eID card:

- `FirstName`, `LastName`, `ThirdName`
- `CardNumber`, `ChipNumber`
- `DateOfBirth`, `CardValidFrom`, `CardValidTo`
- `Nationality`, `PlaceOfBirth`, `Sex`
- `Address`, `PostalCode`
- `CardDeliveryMunicipality`
- And more...

### Exceptions

The library provides specific exception types for different error scenarios:

- `EIdAdapterNotPresentException` - Reader/adapter not found
- `EIdCardNotPresentException` - No card inserted
- `EIdCardNotSupportException` - Card not supported
- `EIdCardReadErrorException` - Error reading card data
- `EIdAdapterNotSupportException` - Reader not supported

## Project Structure

```
Omnicasa.Mobile.Zetes.Maui/
├── Omnicasa.Mobile.Zetes.Standard/     # Core interfaces and models (.NET Standard)
├── Omnicasa.Mobile.Zetes/              # Main MAUI library
│   ├── Platforms/
│   │   ├── iOS/                        # iOS implementation
│   │   └── Android/                    # Android implementation
├── Omnicasa.Mobile.Zetes.iOS/          # iOS native bindings
│   ├── Libs/
│   │   └── libeID-SDK.a                # Native iOS library
│   ├── ApiDefinition.cs                 # Objective-C bindings
│   └── StructsAndEnums.cs              # Type definitions
├── Omnicasa.Mobile.Zetes.Droid/        # Android native bindings
│   ├── Libs/
│   │   └── zseidlib-release.aar         # Native Android library
│   └── Transforms/                     # Binding metadata
├── Omnicasa.Mobile.Zetes.Sample/       # Sample application
└── Demo/                                # Native iOS demo reference
```

## Building

### Prerequisites

1. Install .NET SDK 8.0.415 (see `global.json`)
2. Install .NET MAUI workload:
   ```bash
   dotnet workload install maui
   ```

### Build Commands

```bash
# Restore dependencies
dotnet restore

# Build all projects
dotnet build

# Build for iOS
dotnet build -f net8.0-ios

# Build for Android
dotnet build -f net8.0-android

# Run sample app (requires device/emulator)
dotnet run --project Omnicasa.Mobile.Zetes.Sample
```

## iOS Configuration

### Info.plist

Add the following to your `Info.plist` for Bluetooth permissions:

```xml
<key>NSBluetoothAlwaysUsageDescription</key>
<string>This app needs Bluetooth access to connect to eID card readers.</string>
<key>NSBluetoothPeripheralUsageDescription</key>
<string>This app needs Bluetooth access to connect to eID card readers.</string>
```

### Entitlements

Ensure your app has Bluetooth entitlements enabled in your `.entitlements` file.

## Android Configuration

### AndroidManifest.xml

Add Bluetooth permissions:

```xml
<uses-permission android:name="android.permission.BLUETOOTH" />
<uses-permission android:name="android.permission.BLUETOOTH_ADMIN" />
<uses-permission android:name="android.permission.BLUETOOTH_SCAN" />
<uses-permission android:name="android.permission.BLUETOOTH_CONNECT" />
```

For Android 12+ (API 31+), you may need to add:

```xml
<uses-permission android:name="android.permission.BLUETOOTH_SCAN" 
    android:usesPermissionFlags="neverForLocation" />
```

## Troubleshooting

### "ignoring reader" Messages

If you see messages like `ignoring reader: FT_00A050181A2F (-59 < -57)`, this is normal. The SDK filters out readers with weak Bluetooth signal strength (below -57 dBm). Move the reader closer or ensure it's powered on.

### Reader Not Detected

1. Ensure the reader is powered on
2. Check Bluetooth is enabled on the device
3. Verify app has Bluetooth permissions
4. Try using `"FT_ANY"` as the preferred reader name (default in the library)

### Build Errors

- Ensure .NET SDK 8.0.415 is installed (check `global.json`)
- Verify .NET MAUI workload is installed: `dotnet workload list`
- Clean and rebuild: `dotnet clean && dotnet build`

## Sample Application

The `Omnicasa.Mobile.Zetes.Sample` project demonstrates basic usage of the library. Run it on a physical device with a compatible eID reader to test functionality.

## Contributing

Contributions are welcome! Please ensure:

1. Code follows the existing style
2. All tests pass
3. Documentation is updated
4. Platform-specific implementations are tested on both iOS and Android

## License

See [LICENSE](LICENSE) file for details.

## Repository

- GitHub: https://github.com/omnicasa/Omnicasa.Mobile.Zetes

## Support

For issues, questions, or contributions, please open an issue on GitHub.

