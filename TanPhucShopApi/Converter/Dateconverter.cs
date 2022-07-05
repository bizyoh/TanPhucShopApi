using System.Text.Json.Serialization;
using System.Globalization;
using System.Text.Json;

namespace TanPhucShopApi.Converter
{
    public class Dateconverter : JsonConverter<DateTime>
    {
        public string formatDate = "dd/MM/yyyy";
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            return DateTime.ParseExact(s, formatDate, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(formatDate));
        }
    }
}
