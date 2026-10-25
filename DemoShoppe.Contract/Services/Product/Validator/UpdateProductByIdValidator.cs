using DemoShoppe.Contract.Services.Product.Request;
using FluentValidation;

namespace DemoShoppe.Contract.Services.Product.Validator
{
    public sealed class UpdateProductByIdValidator : AbstractValidator<UpdateProductByIdCommand>
    {
        public UpdateProductByIdValidator()
        {
            RuleFor(x => x.price)
                .GreaterThan(0).WithMessage("Giá phải lớn hơn 0");
            RuleFor(x => x.quantity)
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0");
        }
    }
}
