using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Auth.Request;
using DemoShoppe.Presentation.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoShoppe.Presentation.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IValidator<RegisterCommand> _registerValidator;
        private readonly IValidator<LoginCommand> _loginValidator;
        public AuthController(ISender sender, IValidator<RegisterCommand> registerValidator,
            IValidator<LoginCommand> loginValidator) :
            base(sender)
        {
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand request,
            CancellationToken cancellation = default)
        {
            var validator = await _registerValidator.ValidateAsync(request, cancellation);
            if (!validator.IsValid)
            {
                var message = string.Join("; ", validator.Errors.Select(e => e.ErrorMessage));
                return HandlerFailure(Result.Failure(Error.BadRequest(message)));
            }
            var result = await _sender.Send(request, cancellation);
            if (result.IsSuccess == false || result.IsFailure == true)
                return HandlerFailure(result);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request,
            CancellationToken cancellation = default)
        {
            var validator = await _loginValidator.ValidateAsync(request, cancellation);
            if (!validator.IsValid)
            {
                var message = string.Join("; ", validator.Errors.Select(e => e.ErrorMessage));
                return HandlerFailure(Result.Failure(Error.BadRequest(message)));
            }
            var result = await _sender.Send(request, cancellation);
            if (result.IsSuccess == false || result.IsFailure == true)
                return HandlerFailure(result);
            return Ok(result);
        }
    }
}
