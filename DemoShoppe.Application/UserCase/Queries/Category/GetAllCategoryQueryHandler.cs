using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Contract.Services.Category.Response;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Queries.Category
{
    public sealed class GetAllCategoryQueryHandler : IQueryHandler<GetAllCategoryCommand, PageResult<GetAllCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Result<PageResult<GetAllCategoryResponse>>> Handle(GetAllCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.pageIndex <= 0 || request.pageSize <= 0)
                return Result.Failure<PageResult<GetAllCategoryResponse>>(Error.BadRequest("Số trang và số lượng phần tử phải lớn hơn 0"));
            var query = _categoryRepository.FindQuery();
            var listCategory = await query.Where(x => x.IsDeleted == false)
                .Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize)
                .Select(c => new GetAllCategoryResponse
                (c.Id, c.Name, c.ImageUrl, c.Description, c.ParentId))
                .ToListAsync();
            var result = new PageResult<GetAllCategoryResponse>
            {
                Items = listCategory,
                PageIndex = request.pageIndex,
                PageSize = request.pageSize,
                TotalItems = await _categoryRepository.TotalItems(cancellationToken)
            };
            return Result.Success(result);
        }
    }
}
