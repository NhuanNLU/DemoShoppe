using DemoShoppe.Contract.Services.Auth.Request;
using DemoShoppe.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoShoppe.Presentation.Controllers
{
    public class AuthController : ApiController
    {
        public AuthController(ISender sender) : base(sender) { }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request,
            CancellationToken cancellation = default)
        {
            var result = await _sender.Send(request, cancellation);
            if (result.IsSuccess == false || result.IsFailure == true)
                return HandlerFailure(result);
            return Ok(result);
        }
    }
}
