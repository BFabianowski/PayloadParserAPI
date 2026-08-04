using SimpleApi.Models;

namespace SimpleApi.Services
{
    public interface IDataProcessing
    {
        Result Process(Request request);
    }
}