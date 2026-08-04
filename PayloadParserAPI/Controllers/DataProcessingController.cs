using Microsoft.AspNetCore.Mvc;
using PayloadParserAPI.Models;
using PayloadParserAPI.Services;

namespace PayloadParserAPI.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class DataProcessingController : ControllerBase
    {
        private readonly IDataProcessing service;

        public DataProcessingController(IDataProcessing service)
        {
            this.service = service;
        }

        [HttpPost("parse-content")]
        [Consumes("application/json")]
        [ProducesResponseType<Response>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult ParseContent([FromBody] Request request)
        {
            var result = service.Process(request);

            if (result.IsSuccess) return Ok(result.Response);

            int statusCode;

            switch (result.ErrorType)
            {
                case ErrorType.UnsupportedType:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
                case ErrorType.InvalidBase64:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
                case ErrorType.InvalidData:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
                case ErrorType.ParseError:
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    break;
                default:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
            }

            return Problem(detail: result.ErrorMessage,
                statusCode: statusCode, title: result.ErrorType.ToString());
        }
    }
}