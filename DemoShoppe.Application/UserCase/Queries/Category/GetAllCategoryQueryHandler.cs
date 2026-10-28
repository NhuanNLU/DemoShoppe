using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Contract.Services.Category.Response;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Queries.Category
{
    public class GetAllCategoryQueryHandler : IQueryHandler<GetAllCategoryCommand, PageResult<GetAllCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Result<PageResult<GetAllCategoryResponse>>> Handle(GetAllCategoryCommand request, CancellationToken cancellationToken)
        {
            var query = _categoryRepository.FindAll();
            var listCategoryDto = await query
                .Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize)
                .Select(c => new GetAllCategoryResponse
                (c.Id, c.Name, c.ImageUrl, c.Description, c.ParentId))
                .ToListAsync();
            var result = new PageResult<GetAllCategoryResponse>
            {
                Items = listCategoryDto,
                PageIndex = request.pageIndex,
                PageSize = request.pageSize,
                TotalItems = await _categoryRepository.TotalItems()
            };
            return Result.Success(result);
        }
    }
}
