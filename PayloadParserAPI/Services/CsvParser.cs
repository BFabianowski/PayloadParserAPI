using CsvHelper;
using PayloadParserAPI.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace PayloadParserAPI.Services
{
    public class CsvParser : IContentParser
    {
        public ContentType Type => ContentType.Csv;

        public IEnumerable<Dictionary<string, object?>> Parse(string data)
        {
            using var reader = new StringReader(data);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var rows = new List<Dictionary<string, object?>>();
            foreach (var row in csv.GetRecords<dynamic>())
            {
                var dict = (IDictionary<string, object>)row;
                rows.Add(dict.ToDictionary(k => k.Key, k => (object?)k.Value));
            }

            return rows;
        }
    }
}