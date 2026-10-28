using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Contract.Services.Category.Response;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Queries.Category
{
    public sealed class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdCommand, GetCategoryByIdResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Result<GetCategoryByIdResponse>> Handle(GetCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindQuery()
                .FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
            if (category is null)
                return Result.Failure<GetCategoryByIdResponse>(Error.NotFound("Không tìm thấy danh mục"));
            var result = new GetCategoryByIdResponse(
                category.Id,
                category.Name,
                category.Description,
                category.ImageUrl);
            return Result.Success(result);
        }
    }
}
