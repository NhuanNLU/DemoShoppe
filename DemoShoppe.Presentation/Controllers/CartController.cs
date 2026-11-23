using DemoShoppe.Contract.Services.Cart.Request;
using DemoShoppe.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoShoppe.Presentation.Controllers
{
    [Authorize]
    public sealed class CartController : ApiController
    {
        public CartController(ISender sender) : base(sender)
        {
        }
        [HttpPost()]
        public async Task<IActionResult> AddToCart
            ([FromBody] AddToCartCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(request, cancellationToken);
            if (!result.IsSuccess || result.IsFailure)
            {
                return HandlerFailure(result);
            }
            return Ok(result);
        }
        [HttpPatch("clear-cart")]
        public async Task<IActionResult> ClearCart([FromQuery] ClearCartCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(request, cancellationToken);
            if (!result.IsSuccess || result.IsFailure)
            {
                return HandlerFailure(result);
            }
            return Ok(result);
        }
        [HttpPatch("remove-products-from-cart")]
        public async Task<IActionResult> RemoveProductsFromCart
            ([FromBody] RemoveProductsFromCartCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(request, cancellationToken);
            if (!result.IsSuccess || result.IsFailure)
            {
                return HandlerFailure(result);
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCart([FromQuery] GetMyCartQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(request, cancellationToken);
            if (!result.IsSuccess || result.IsFailure)
            {
                return HandlerFailure(result);
            }
            return Ok(result);
        }
    }
}
