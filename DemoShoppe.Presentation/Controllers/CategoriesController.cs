using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoShoppe.Presentation.Controllers
{
    public class CategoriesController : ApiController
    {
        public CategoriesController(ISender sender) : base(sender) { }
        [HttpPost]
        public async Task<IActionResult> CreateCategory
            (CreateCategoryRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(request, cancellationToken);
            if (result.IsSuccess == false || result.IsFailure == true)
                return HandlerFailure(result);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory
            ([FromQuery] GetAllCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(request, cancellationToken);
            if (result.IsSuccess == false || result.IsFailure == true)
                return HandlerFailure(result);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById
            ([FromRoute] GetCategoryByIdRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(request, cancellationToken);
            if (result.IsSuccess == false || result.IsFailure == true)
                return HandlerFailure(result);
            return Ok(result);
        }
    }
}
