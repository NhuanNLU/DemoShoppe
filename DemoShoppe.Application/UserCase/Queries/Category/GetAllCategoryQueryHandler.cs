using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Contract.Services.Category.Response;
using DemoShoppe.Domain.Abstractions.Repositories;

namespace DemoShoppe.Application.UserCase.Queries.Category
{
    public class GetAllCategoryQueryHandler : IQueryHandler<GetAllCategoryRequest, PageResult<GetAllCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Result<PageResult<GetAllCategoryResponse>>> Handle(GetAllCategoryRequest request, CancellationToken cancellationToken)
        {
            var getAllCategory = await _categoryRepository.FindAll(cancellationToken);
            var listCategoryDto = getAllCategory
                .Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(c => new GetAllCategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.ImageUrl,
                    Description = c.Description,
                    ParentId = c.ParentId
                }).ToList();
            var result = new PageResult<GetAllCategoryResponse>
            {
                Items = listCategoryDto,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalItems = await _categoryRepository.CountCategory()
            };
            return Result.Success(result);
        }
    }
}
