namespace NativeAppStore;

public class StoreSaveExecutor
{
    internal static Action OnApplicationExit { get; set; }

    public static void SaveStore()
    {
        OnApplicationExit?.Invoke();
    }
}