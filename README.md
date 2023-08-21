# NativeAppStore

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://mit-license.org/)
[![GitHub Stars](https://img.shields.io/github/stars/zyknow/NativeAppStore.svg)](https://github.com/zyknow/NativeAppStore/stargazers)
[![GitHub Issues](https://img.shields.io/github/issues/zyknow/NativeAppStore.svg)](https://github.com/zyknow/NativeAppStore/issues)

## Introduction

in .net build Native App,you may want to save some data in local, so you can use this package to save data in local.

## Nuget Packages

| Name                  | Version                                                                                                                                     | Download                                                                                                                                     |
|-----------------------|---------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------|
| Zyknow.NativeAppStore | [![Zyknow.NativeAppStore](https://img.shields.io/nuget/v/Zyknow.NativeAppStore.svg)](https://www.nuget.org/packages/Zyknow.NativeAppStore/) | [![Zyknow.NativeAppStore](https://img.shields.io/nuget/dt/Zyknow.NativeAppStore.svg)](https://www.nuget.org/packages/Zyknow.NativeAppStore/) |

## Guide

in many ui framework, exit hooks many cannot must be invoke （like anrdoid）,so you may need give the save decision to
user,or global
exception catcher to save stores.

or if you have any ideas,Welcome to create this rep [Issues](https://github.com/zyknow/NativeAppStore/issues)

## Usage

1. Add Package Reference `Zyknow.NativeAppStore`
2. Add Services

```csharp
services.AddStores(GetType().Assembly, opt => { opt.EnabledCreatorStoreLoad = true; });
```

3. Create Store

```csharp
public class MainWindowStore : StoreBase
{
  
}
```

4. To Save Store On App Exit Or Global Exception Catch

```csharp
// that will save all stores
StoreSaveExecutor.SaveAllStores();

// or invoke store save
store.SaveStore();
```

## Framework Tests

### Avalonia

* Desktop
    * [x] Windows
    * [ ] Mac
    * [ ] Linux
* Mobile
    * [x] Android
    * [ ] iOS
* ~~WebAssembly~~

### Maui

* Desktop
    * [x] Windows
    * [ ] Mac
    * [ ] Windows
* Mobile
    * [ ] Android
    * [ ] iOS

## Author

[Zyknow](https://github.com/zyknow)

## License

> You can check out the full license [here](https://github.com/zyknow/NativeAppStore/blob/master/LICENSE)

This project is licensed under the terms of the **MIT** license.
