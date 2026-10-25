using DemoShoppe.Contract.Services.Product.Request;
using FluentValidation;

namespace DemoShoppe.Contract.Services.Product.Validator
{
    public sealed class CreateProductValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.name)
                 .NotEmpty().WithMessage("Tên sản phẩm không được để trống");
            RuleFor(x => x.price)
                .NotEmpty().WithMessage("Giá sản phẩm không được để trống")
                .GreaterThan(0).WithMessage("Giá phải lớn hơn 0");
            RuleFor(x => x.quantity)
                .NotEmpty().WithMessage("Số lượng sản phẩm không được để trống")
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0");
        }
    }
}
