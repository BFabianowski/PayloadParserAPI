using PayloadParserAPI.Models;

namespace PayloadParserAPI.Services
{
    public interface IContentParser
    {
        ContentType Type { get; }
        IEnumerable<Dictionary<string, object?>> Parse(string data);
    }
}