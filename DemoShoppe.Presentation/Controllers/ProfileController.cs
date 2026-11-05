using DemoShoppe.Contract.Services.Profile.Request;
using DemoShoppe.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoShoppe.Presentation.Controllers
{
    public class ProfileController : ApiController
    {
        public ProfileController(ISender sender) : base(sender)
        {

        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe([FromQuery] GetMeQuery request, CancellationToken cancellation = default)
        {
            var result = await _sender.Send(request, cancellation);
            if (result.IsSuccess == false || result.IsFailure == true)
                return HandlerFailure(result);
            return Ok(result);
        }
    }
}
