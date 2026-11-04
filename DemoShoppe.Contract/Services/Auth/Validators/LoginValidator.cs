using DemoShoppe.Contract.Services.Auth.Request;
using FluentValidation;

namespace DemoShoppe.Contract.Services.Auth.Validators
{
    public sealed class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không đúng định dạng")
                .MaximumLength(254).WithMessage("Email tối đa 254 ký tự");
            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(8).WithMessage("Mật khẩu tối thiểu 8 ký tự")
                .MaximumLength(64).WithMessage("Mật khẩu tối đa 64 ký tự")
                .Matches("[A-Z]").WithMessage("Mật khẩu phải có ít nhất 1 chữ hoa")
                .Matches("[a-z]").WithMessage("Mật khẩu phải có ít nhất 1 chữ thường")
                .Matches("[0-9]").WithMessage("Mật khẩu phải có ít nhất 1 chữ số")
                .Matches("[^A-Za-z0-9]").WithMessage("Mật khẩu phải có ít nhất 1 ký tự đặc biệt");
        }
    }
}
