namespace NativeAppStore.Core;

public interface IStore
{

    public void SaveStore();
    public void LoadStore();

    public void ResetStore();

    /// <summary>
    ///  will delete the store file
    /// </summary>
    void ClearStore();
}