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
                .MaximumLength(32).WithMessage("Tên danh mục quá dài");
        }
    }
}
