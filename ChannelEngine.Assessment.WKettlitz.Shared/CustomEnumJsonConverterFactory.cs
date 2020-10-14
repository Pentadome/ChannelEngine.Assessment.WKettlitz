using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChannelEngine.Assessment.WKettlitz.Shared
{
    public class CustomEnumJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert is null)
            {
                throw new ArgumentNullException(nameof(typeToConvert));
            }

            return typeToConvert.IsEnum;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert is null)
            {
                throw new ArgumentNullException(nameof(typeToConvert));
            }

            if (!typeToConvert.IsEnum)
                throw new InvalidOperationException();

            return (JsonConverter)Activator.CreateInstance(
                typeof(EnumJsonConverter<>).MakeGenericType(typeToConvert))!;
        }

        public class EnumJsonConverter<T> : JsonConverter<T> where T : struct, Enum
        {
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var enumString = reader.GetString();

                // Todo: cache look-ups for performance
                var field = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
                    .First(x =>
                    {
                        var attribute = x.GetCustomAttribute<EnumMemberAttribute>();

                        if (!(attribute is null))
                        {
                            return attribute.Value.Equals(enumString, StringComparison.OrdinalIgnoreCase);
                        }
                        return x.Name.Equals(enumString, StringComparison.OrdinalIgnoreCase);
                    });

                return (T)field.GetValue(null)!;
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                var field = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
                                .First(x => ((T)x.GetValue(null)!).Equals(value));
                var attribute = field.GetCustomAttribute<EnumMemberAttribute>();
                if (attribute is null)
                {
                    var namingPolicy = options.PropertyNamingPolicy ?? JsonNamingPolicy.CamelCase;
                    writer.WriteStringValue(namingPolicy.ConvertName(field.Name));
                }
                else
                {
                    writer.WriteStringValue(attribute.Value);
                }
            }
        }
    }
}
