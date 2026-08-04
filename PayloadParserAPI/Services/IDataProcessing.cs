using PayloadParserAPI.Models;

namespace PayloadParserAPI.Services
{
    public interface IDataProcessing
    {
        Result Process(Request request);
    }
}