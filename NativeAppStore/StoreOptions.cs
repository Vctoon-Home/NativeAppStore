namespace NativeAppStore;

public class StoreOptions
{
    public string EncryptionKey { get; set; } = "NativeAppStore";

    public string DefaultRootFolderPath { get; set; } = "AppStores";

    public bool EnabledCreatorStoreLoad { get; set; }
}