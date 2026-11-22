using DemoShoppe.Contract.Services.Cart.Request;
using DemoShoppe.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoShoppe.Presentation.Controllers
{
    public sealed class CartController : ApiController
    {
        public CartController(ISender sender) : base(sender)
        {
        }
        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> AddToCart
            ([FromBody] AddToCartCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
