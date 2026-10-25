using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Presentation.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoShoppe.Presentation.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly IValidator<CreateCategoryCommand> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryByIdCommand> _updateCategoryByIdValidator;
        public CategoriesController(ISender sender,
            IValidator<CreateCategoryCommand> createCategoryValidator,
            IValidator<UpdateCategoryByIdCommand> updateCategoryByIdValidator
            ) : 
            base(sender) 
        {
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryByIdValidator = updateCategoryByIdValidator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory
            ([FromBody]CreateCategoryCommand request,
            CancellationToken cancellationToken = default)
        {
            var validator = await _createCategoryValidator.ValidateAsync(request, cancellationToken);
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
        public async Task<IActionResult> GetAllCategory
            ([FromQuery] GetAllCategoryCommand request,
            CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(request, cancellationToken);
            if (result.IsSuccess == false || result.IsFailure == true)
                return HandlerFailure(result);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById
            ([FromRoute] GetCategoryByIdCommand request,
            CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(request, cancellationToken);
            if (result.IsSuccess == false || result.IsFailure == true)
                return HandlerFailure(result);
            return Ok(result);
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateCategoryById
            ([FromBody]UpdateCategoryByIdCommand request,
            CancellationToken cancellationToken = default)
        {
            var validator = await _updateCategoryByIdValidator.ValidateAsync(request, cancellationToken);
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
    }
}
