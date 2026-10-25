using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Product.Request;
using DemoShoppe.Presentation.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoShoppe.Presentation.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IValidator<CreateProductCommand> _createProductValidator;
        public ProductsController(ISender sender,
            IValidator<CreateProductCommand> createProductValidator)
            : base(sender)
        {
            _createProductValidator = createProductValidator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand request, CancellationToken cancellationToken = default)
        {
            var validator = await _createProductValidator.ValidateAsync(request, cancellationToken);
            if (!validator.IsValid)
            {
                var message = string.Join("; ",
                    validator.Errors.Select(e => e.ErrorMessage));
                return HandlerFailure(Result.Failure(Error.BadRequest(message)));
            }
            var result = await _sender.Send(request, cancellationToken);
            if (result.IsSuccess == false || result.IsFailure == true)
                return HandlerFailure(result);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct(GetAllProductCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(request, cancellationToken);
            if (result.IsSuccess == false || result.IsFailure == true)
                return HandlerFailure(result);
            return Ok(result);
        }
    }
}
