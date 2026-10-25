using DemoShoppe.Contract.Services.Category.Request;
using FluentValidation;

namespace DemoShoppe.Contract.Services.Category.Validators
{
    public sealed class UpdateCategoryByIdValidator :
        AbstractValidator<UpdateCategoryByIdCommand>
    {
        public UpdateCategoryByIdValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Tên danh mục không được để trống")
                .MaximumLength(32).WithMessage("Tên danh mục quá dài");
        }
    }
}
