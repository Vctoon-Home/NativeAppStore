using System.Reflection;
using System.Runtime.Serialization;
using NativeAppStore.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NativeAppStore.Core;

public class StoreConverter : JsonConverter
{
    private static StoreResolver resolver = new StoreResolver();

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        Type type = value.GetType();

        if (type.GetSingleAttributeOrNull<StoreIgnoreAttribute>() != null)
            return;

        writer.WriteStartObject();

        List<MemberInfo> members = resolver.GetRuleMembers(type).ToList();

        // 检查类是否有 StoreEncryptionAttribute 特性
        bool isEncryptClass = type.GetCustomAttribute(typeof(StoreEncryptAttribute)) != null;

        foreach (var member in members)
        {
            // 检查属性是否有 StoreIgnoreAttribute 特性
            if (member.GetCustomAttribute(typeof(StoreIgnoreAttribute)) != null)
                continue;

            string memberName = member.Name;
            // object memberValue = member.GetValue(value);
            object memberValue = resolver.GetMemberValueOnConvertor(member, value, isEncryptClass);


            writer.WritePropertyName(memberName);
            writer.WriteValue(memberValue);
        }

        writer.WriteEndObject();
    }


    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        var result = FormatterServices.GetUninitializedObject(objectType);


        if (objectType.GetSingleAttributeOrNull<StoreIgnoreAttribute>() != null)
            return result;


        // 检查类是否有 StoreEncryptionAttribute 特性
        bool isEncryptClass = objectType.GetCustomAttribute(typeof(StoreEncryptAttribute)) != null;

        var members = resolver.GetRuleMembers(objectType).ToList();
        foreach (var member in members)
        {
            var ignoreAttribute = member.GetCustomAttribute(typeof(StoreIgnoreAttribute));
            if (ignoreAttribute != null)
                continue;

            var storeAttribute = member.GetCustomAttribute(typeof(StoreAttribute));

            if (storeAttribute != null || obj[member.Name] != null)
            {
                resolver.SetMemberValueOnConvertor(member, result, obj[member.Name], isEncryptClass);
            }
        }

        return result;
    }


    public override bool CanConvert(Type objectType)
    {
        return true;
    }
}