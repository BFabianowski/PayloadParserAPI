using SimpleApi.Models;
using System.Net.Mime;

namespace SimpleApi.Services
{
    public interface IContentParser
    {
        ContentType Type { get; }
        IEnumerable<Dictionary<string, object?>> Parse(string data);
    }
}