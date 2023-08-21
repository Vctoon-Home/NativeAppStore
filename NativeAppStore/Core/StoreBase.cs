using NativeAppStore.Consts;
using NativeAppStore.Extensions;
using Newtonsoft.Json;

namespace NativeAppStore.Core;

public abstract class StoreBase : IStore
{

    private bool onApplicationExitIsRegister { get; set; }
    private bool? ignore = null;
    private bool IgnoreStore
    {
        get
        {
            if (ignore == null)
            {
                ignore = this.GetType().GetSingleAttributeOrNull<StoreIgnoreAttribute>() != null;
            }


            return ignore.Value;
        }
    }

    protected string _savePath;

    protected string SavePath
    {
        get
        {
            if (string.IsNullOrEmpty(_savePath))
                _savePath = GetSaveFilePath();

            return _savePath;
        }
        set => _savePath = value;
    }


    private string GetCurrentOsDefaultSavePath()
    {
        var dirPath = Path.Combine(Os.IsWindows || Os.IsLinux
                ? Environment.CurrentDirectory
                : Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            StoreExtensions.StoreOptions.DefaultRootFolderPath);

        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);

        return dirPath;
    }

    private string GetSaveFilePath()
    {
        var folderPath = GetCurrentOsDefaultSavePath();

        var fileName = this.GetType().Name.Replace("Store", "") + ".json";

        return Path.Combine(folderPath, fileName);
    }

    public void SaveStore()
    {
        if (IgnoreStore)
        {
            return;
        }


        var path = SavePath;

        // 获取path的目录路径
        var dirPath = Path.GetDirectoryName(path);
        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath!);


        if (File.Exists(path))
            File.Delete(path);

        PreSaveStore();

        var str = JsonConvert.SerializeObject(this, new StoreConverter());

        File.WriteAllText(path, str);

        PostSaveStore();
    }

    public void LoadStore()
    {
        if (IgnoreStore)
        {
            return;
        }

        RegisterOnApplicationExit();

        var path = SavePath;

        if (!File.Exists(path))
            return;

        PreLoadStore();

        var type = GetType();

        var json = File.ReadAllText(path);

        var obj = JsonConvert.DeserializeObject(json, type, new StoreConverter());

        // obj is of type this type, but the value is the value of obj, so all the values of obj need to be assigned to this

        var resolve = new StoreResolver();

        var members = resolve.GetRuleMembers(type);
        foreach (var member in members)
        {
            resolve.SetMemberValue(member, this, resolve.GetMemberValue(member, obj));
        }

        PreSaveStore();
    }

    /// <summary>
    ///  will delete the store file
    /// </summary>
    public void ClearStore()
    {
        var path = SavePath;
        if (File.Exists(path))
            File.Delete(path);
    }

    public virtual void PreSaveStore()
    {
    }

    public virtual void PostLoadStore()
    {
    }

    public virtual void PostSaveStore()
    {
    }

    public virtual void PreLoadStore()
    {
    }

    public virtual void ResetStore()
    {
    }

    private void RegisterOnApplicationExit()
    {
        if (onApplicationExitIsRegister)
            return;

        onApplicationExitIsRegister = true;


        StoreSaveExecutor.OnApplicationExit += SaveStore;
    }
}