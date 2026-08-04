using SimpleApi.Models;
using System.Net.Mime;
using System.Text;
using ContentType = SimpleApi.Models.ContentType;

namespace SimpleApi.Services
{
    public class DataProcessing : IDataProcessing
    {
        private readonly IDictionary<ContentType, IContentParser> parsers;

        public DataProcessing(IEnumerable<IContentParser> parsers)
        {
            this.parsers = parsers.ToDictionary(x => x.Type);
        }

        public Result Process(Request request)
        {
            if (string.IsNullOrEmpty(request.Type) || string.IsNullOrEmpty(request.Content))
            {
                return Result.Failure(ErrorType.InvalidData, "Niepoprawne dane");
            }

            var type = request.Type;

            ContentType contentType;
            IContentParser parser = null;

            switch (type)
            {
                case "CSV":
                    contentType = ContentType.Csv;
                    _ = parsers.TryGetValue(contentType, out parser);
                    break;
                case "INTERNAL_JSON":
                    contentType = ContentType.InternalJson;
                    _ = parsers.TryGetValue(contentType, out parser);
                    break;
            }

            if (parser == null)
            {
                return Result.Failure(ErrorType.UnsupportedType, $"Nieobsługiwany typ {type}");
            }

            string decodedData;
            try
            {
                var bytes = Convert.FromBase64String(request.Content ?? string.Empty);
                decodedData = Encoding.UTF8.GetString(bytes);
            }
            catch (FormatException e)
            {
                return Result.Failure(ErrorType.InvalidBase64, $"Content nie jest poprawne. Szczegóły błędu: {e.Message}");
            }

            try
            {
                var parsedData = parser.Parse(decodedData);
                var response = new Response("success", type, parsedData.Count(), parsedData);
                return Result.Success(response);
            }
            catch (Exception ex)
            {
                return Result.Failure(ErrorType.ParseError, $"Nie udało się sparsować danych. Błąd: {ex.Message}");
            }
        }
    }
}