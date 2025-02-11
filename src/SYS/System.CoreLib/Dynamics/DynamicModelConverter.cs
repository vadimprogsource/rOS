using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace System.CoreLib.Dynamics;

public class DynamicModelConverter : JsonConverter<DynamicModel>
{



    private static DynamicModel ReadObject(JsonElement root)
    {
        DynamicModel model = new ();


        foreach (JsonProperty prop in root.EnumerateObject())
        {
            model[prop.Name] = ReadValue(prop.Value);

        }

        return model;
    }

    private static object? ReadValue(JsonElement value) => value.ValueKind switch
    {
        JsonValueKind.Array => ReadArray(value),
        JsonValueKind.Null => null,
        JsonValueKind.Number => value.GetDecimal(),
        JsonValueKind.Object => ReadObject(value),
        JsonValueKind.String => value.GetString(),
        JsonValueKind.True => true,
        JsonValueKind.False => false,
        _ => value.GetString(),
    };



    private static Array ReadArray(JsonElement array)
    {
        List<object?> list = new ();

        foreach (JsonElement item in array.EnumerateArray())
        {
            list.Add(ReadValue(item));
        }

        return list.ToArray();
    }



    public override DynamicModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        JsonDocument doc = JsonDocument.ParseValue(ref reader);
        DynamicModel model = ReadObject(doc.RootElement);
        return model;
    }

    public override void Write(Utf8JsonWriter writer, DynamicModel value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var (key, val) in value.ToDictionary())
        {
            writer.WritePropertyName(key);

            if (val == null)
            {
                writer.WriteNullValue();
                continue;
            }

            writer.WriteStringValue(val.ToString());
        }

        writer.WriteEndObject();

    }
}
