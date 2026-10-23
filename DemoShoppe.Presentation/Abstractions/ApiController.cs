using DemoShoppe.Contract.Abstractions.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoShoppe.Presentation.Abstractions
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender _sender;
        protected ApiController(ISender sender)
        {
            _sender = sender;
        }
        protected IActionResult HandlerFailure(Result result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException(
                    "HandlerFailure chỉ dùng cho kết quả thất bại.");

            var statusCode = result.Error.StatusCode == 0
                ? StatusCodes.Status400BadRequest
                : result.Error.StatusCode;

            var title = statusCode switch
            {
                StatusCodes.Status400BadRequest => "Bad Request",
                StatusCodes.Status401Unauthorized => "Unauthorized",
                StatusCodes.Status403Forbidden => "Forbidden",
                StatusCodes.Status404NotFound => "Not Found",
                StatusCodes.Status409Conflict => "Conflict",
                _ => "Internal Server Error"
            };

            return new ObjectResult(new ProblemDetails
            {
                Title = title,
                Detail = result.Error.Message,
                Status = statusCode
            })
            {
                StatusCode = statusCode
            };
        }
    }
}
