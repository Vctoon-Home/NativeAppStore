using NativeAppStore.Core;

namespace NativeAppStore.Test.Models;

public class CompObject
{
    public string Name { get; set; }
}

public class SimpleStore : StoreBase
{
    public SimpleStore(string savePath)
    {
        _savePath = savePath;
    }


    [Store]
    private string StorePrivateFieldStr;

    public void SetStorePrivateFieldStr(string value)
    {
        StorePrivateFieldStr = value;
    }

    public bool IsSameStorePrivateFieldStr(SimpleStore store)
    {
        return StorePrivateFieldStr == store.StorePrivateFieldStr;
    }

    [Store]
    public string StorePublicFieldStr;

    [Store]
    private string StorePrivateStr { get; set; }

    public void SetStorePrivateStr(string value)
    {
        StorePrivateStr = value;
    }

    public bool IsSameStorePrivateStr(SimpleStore store)
    {
        return StorePrivateStr == store.StorePrivateStr;
    }


    [StoreIgnore]
    public string StoreIgnorePublicStr { get; set; }

    [StoreEncrypt]
    private string StoreEncryptionPrivateStr { get; set; }

    public void SetStoreEncryptionPrivateStr(string value)
    {
        StoreEncryptionPrivateStr = value;
    }

    public bool IsSameStoreEncryptionPrivateStr(SimpleStore store)
    {
        return StoreEncryptionPrivateStr == store.StoreEncryptionPrivateStr;
    }

    
    public bool IsSameStoreEncryptionPrivateStr(string str)
    {
        return StoreEncryptionPrivateStr == str;
    }


    public string StorePublicStr { get; set; }

    public string? StorePublicNullStr { get; set; }

    public int? StorePublicNullInt { get; set; }

    public long? StorePublicNullLong { get; set; }

    public float? StorePublicNullFloat { get; set; }

    public double? StorePublicNullDouble { get; set; }

    public decimal? StorePublicNullDecimal { get; set; }

    public bool? StorePublicNullBool { get; set; }

    public DateTime? StorePublicNullDateTime { get; set; }

    public DateTimeOffset? StorePublicNullDateTimeOffset { get; set; }

    public TimeSpan? StorePublicNullTimeSpan { get; set; }

    public CompObject? StorePublicNullCompObject { get; set; }

    public int StorePublicInt { get; set; }

    public long StorePublicLong { get; set; }

    public float StorePublicFloat { get; set; }

    public double StorePublicDouble { get; set; }

    public decimal StorePublicDecimal { get; set; }

    public bool StorePublicBool { get; set; }

    public DateTime StorePublicDateTime { get; set; }

    public DateTimeOffset StorePublicDateTimeOffset { get; set; }

    public TimeSpan StorePublicTimeSpan { get; set; }

    public CompObject StorePublicCompObject { get; set; }


    [StoreEncrypt]
    public string StoreEncryptionPublicStr { get; set; }

    [StoreEncrypt]
    public int StoreEncryptionPublicInt { get; set; }
}