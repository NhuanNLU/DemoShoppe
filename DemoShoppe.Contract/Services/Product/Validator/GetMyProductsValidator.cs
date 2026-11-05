using DemoShoppe.Contract.Services.Product.Request;
using FluentValidation;

namespace DemoShoppe.Contract.Services.Product.Validator
{
    public sealed class GetMyProductsValidator: AbstractValidator<GetMyProductsQuery>
    {
        public GetMyProductsValidator()
        {
            RuleFor(x => x.pageIndex).GreaterThan(0).WithMessage("page index phải lớn hơn 0");
            RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("page size phải lớn hơn 0");
        }
    }
}
