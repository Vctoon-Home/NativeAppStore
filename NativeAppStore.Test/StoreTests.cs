using NativeAppStore.Test.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shouldly;

namespace NativeAppStore.Test;

public class StoreTests
{

    [Fact]
    public void Should_Save_Store()
    {
        var filePath = Path.Combine("Settings", "test.json");

        // Arrange
        var store = new SimpleStore(filePath);

        // set public members
        store.StorePublicBool = true;
        store.StorePublicCompObject = new CompObject {Name = "test"};
        store.StorePublicDateTime = DateTime.Now;
        store.StorePublicDateTimeOffset = DateTimeOffset.Now;
        store.StorePublicDecimal = 1.1m;
        store.StorePublicDouble = 1.1;
        store.StorePublicFloat = 1.1f;
        store.StorePublicInt = 1;
        store.StorePublicLong = 1;
        store.StorePublicStr = "test";
        store.StorePublicTimeSpan = TimeSpan.FromDays(1);

        store.StoreEncryptionPublicStr = "test";
        store.StoreEncryptionPublicInt = 5;

        store.StorePublicFieldStr = "test";

        // set private members
        store.SetStoreEncryptionPrivateStr("test");
        store.SetStorePrivateStr("test");
        store.SetStorePrivateFieldStr("test");


        // Act
        store.SaveStore();

        // Assert
        var store2 = new SimpleStore(filePath);
        store2.LoadStore();

        // public items should be same
        store2.StorePublicFieldStr.ShouldBe(store.StorePublicFieldStr);

        store2.StorePublicBool.ShouldBe(store.StorePublicBool);
        store2.StorePublicCompObject.Name.ShouldBe(store.StorePublicCompObject.Name);
        store2.StorePublicDateTime.ShouldBe(store.StorePublicDateTime);
        store2.StorePublicDateTimeOffset.ShouldBe(store.StorePublicDateTimeOffset);
        store2.StorePublicDecimal.ShouldBe(store.StorePublicDecimal);
        store2.StorePublicDouble.ShouldBe(store.StorePublicDouble);
        store2.StorePublicFloat.ShouldBe(store.StorePublicFloat);
        store2.StorePublicInt.ShouldBe(store.StorePublicInt);
        store2.StorePublicLong.ShouldBe(store.StorePublicLong);
        store2.StorePublicStr.ShouldBe(store.StorePublicStr);
        store2.StorePublicTimeSpan.ShouldBe(store.StorePublicTimeSpan);

        // encryption items should be same
        store2.StoreEncryptionPublicStr.ShouldBe(store.StoreEncryptionPublicStr);
        store2.StoreEncryptionPublicInt.ShouldBe(store.StoreEncryptionPublicInt);
        store2.IsSameStoreEncryptionPrivateStr(store).ShouldBe(true);


        // physical file encryption data should be not same
        {
            var stream = new StreamReader(filePath);
            var jobj = JObject.Load(new JsonTextReader(stream));


            var storeEncryptionPublicStr = jobj["StoreEncryptionPublicStr"].Value<string>();
            storeEncryptionPublicStr.ShouldNotBe(store.StoreEncryptionPublicStr);

            var storeEncryptionPrivateStr = jobj["StoreEncryptionPrivateStr"].Value<string>();
            store.IsSameStoreEncryptionPrivateStr(storeEncryptionPrivateStr)
                .ShouldBe(false);

            var storeEncryptionPublicInt = jobj["StoreEncryptionPublicInt"].Value<int>();
            storeEncryptionPublicInt.ShouldBe(store.StoreEncryptionPublicInt);

            // private items should be same
            store2.IsSameStorePrivateStr(store).ShouldBe(true);
            store2.IsSameStorePrivateFieldStr(store).ShouldBe(true);

            stream.Dispose();
        }


        // nullable properties should be same
        store2.StorePublicNullBool.ShouldBe(store.StorePublicNullBool);
        store2.StorePublicNullCompObject.ShouldBe(store.StorePublicNullCompObject);
        store2.StorePublicNullDateTime.ShouldBe(store.StorePublicNullDateTime);
        store2.StorePublicNullDateTimeOffset.ShouldBe(store.StorePublicNullDateTimeOffset);
        store2.StorePublicNullDecimal.ShouldBe(store.StorePublicNullDecimal);
        store2.StorePublicNullDouble.ShouldBe(store.StorePublicNullDouble);
        store2.StorePublicNullFloat.ShouldBe(store.StorePublicNullFloat);
        store2.StorePublicNullInt.ShouldBe(store.StorePublicNullInt);
        store2.StorePublicNullLong.ShouldBe(store.StorePublicNullLong);
        store2.StorePublicNullStr.ShouldBe(store.StorePublicNullStr);
        store2.StorePublicNullTimeSpan.ShouldBe(store.StorePublicNullTimeSpan);


        File.Delete(filePath);
    }
}