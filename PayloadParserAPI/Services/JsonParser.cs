using PayloadParserAPI.Services;
using PayloadParserAPI.Models;
using System.Text.Json;

namespace PayloadParserAPI.Services
{
    public class JsonParser : IContentParser
    {
        public ContentType Type => ContentType.InternalJson;

        public IEnumerable<Dictionary<string, object?>> Parse(string data)
        {
            using var doc = JsonDocument.Parse(data);
            var root = doc.RootElement;

            var fileType = root.ValueKind;

            var elements = root.ValueKind switch
            {
                JsonValueKind.Array => root.EnumerateArray(),
                JsonValueKind.Object => Wrap(root),
                _ => throw new JsonException(
                    "Oczekiwano tablicy obiektów lub pojedynczego obiektu JSON.")
            };

            var rows = new List<Dictionary<string, object?>>();
            foreach (var element in elements)
            {
                var dict = new Dictionary<string, object?>();
                foreach (var prop in element.EnumerateObject())
                {
                    dict[prop.Name] = Normalize(prop.Value);
                }
                rows.Add(dict);
            }

            return rows;
        }

        private static object? Normalize(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.String:
                    return element.GetString();
                case JsonValueKind.Number:
                    return element.TryGetInt64(out var number) ? number : element.GetDouble();
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.False:
                    return false;
                case JsonValueKind.Null:
                    return null;
                default:
                    return element.GetRawText();
            }
        }

        private static IEnumerable<JsonElement> Wrap(JsonElement obj)
        {
            yield return obj;
        }
    }
}