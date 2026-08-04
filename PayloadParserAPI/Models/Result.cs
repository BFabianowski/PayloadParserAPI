using PayloadParserAPI.Models;

namespace SimpleApi.Models
{
    public enum ErrorType
    {
        UnsupportedType,
        InvalidBase64,
        ParseError,
        InvalidData
    }

    public record Result
    {
        public bool IsSuccess { get; private init; }
        public Response? Response { get; private init; }
        public ErrorType? ErrorType { get; private init; }
        public string? ErrorMessage { get; private init; }

        public static Result Success(Response response)
        {
            return new Result()
            {
                IsSuccess = true,
                Response = response
            };
        }

        public static Result Failure(ErrorType type, string message)
        {
            return new Result()
            {
                IsSuccess = false,
                ErrorType = type,
                ErrorMessage = message
            };
        }
    }
}