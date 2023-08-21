namespace NativeAppStore.Core;

public interface IStore
{

    public void SaveStore();
    public void LoadStore();

    public void PreSaveStore();
    public void PostLoadStore();


    public void PostSaveStore();
    public void PreLoadStore();


    public void ResetStore();

    /// <summary>
    ///  will delete the store file
    /// </summary>
    void ClearStore();
}