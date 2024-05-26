using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pingo.Networking.Java.Protocol.Components;

namespace Pingo.Converters;


public class DescriptionConverter : JsonConverter<Description>
{
    public override Description Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var description = new Description();

        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                description.Text = reader.GetString() ?? string.Empty;
                description.Extra = [];
                break;
            case JsonTokenType.StartObject:
                // Read through the object and get its properties accordingly
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string propertyName = reader.GetString();

                        reader.Read();

                        switch (propertyName)
                        {
                            case "text":
                                description.Text = reader.GetString();
                                break;
                            case "extra":
                                description.Extra = JsonSerializer.Deserialize<ChatMessage[]>(ref reader, options);
                                break;
                        }
                    }

                    // If it's the end of the object, then break out of the loop
                    else if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        break;
                    }
                }
                break;
        }

        return description;
    }

    public override void Write(Utf8JsonWriter writer, Description description, JsonSerializerOptions options)
    {
        // Implement your custom logic to serialize a Description instance into a JSON.
        throw new NotImplementedException();
    }
}
