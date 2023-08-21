namespace NativeAppStore;

public class StoreOptions
{
    /// <summary>
    /// 
    /// </summary>
    public string EncryptionKey { get; set; } = "NativeAppStore";

    /// <summary>
    /// 
    /// </summary>
    public string DefaultRootFolderPath { get; set; } = "AppStores";

    /// <summary>
    /// when true, store will be first get to load,if this is false, you need manual to invoke LoadStore method
    /// </summary>
    public bool EnabledCreatorStoreLoad { get; set; }
}