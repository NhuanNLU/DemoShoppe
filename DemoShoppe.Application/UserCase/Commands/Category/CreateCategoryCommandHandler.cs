using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Contract.Services.Category.Response;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Commands.Category
{
    public sealed class CreateCategoryCommandHandler :
        ICommandHandler<CreateCategoryCommand, CreateCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.CategoryNameExists(request.name))
                return Result.Failure<CreateCategoryResponse>(Error.Conflict("Tên danh mục này đã tồn tại"));
            if (request.parentId is not null)
            {
                if (!await _categoryRepository.FindQuery().AnyAsync(x => x.Id == request.parentId.Value))
                    return Result.Failure<CreateCategoryResponse>(Error.NotFound("Không tìm thấy danh mục"));
            }
            var newCategory = new Domain.Entities.Category
            {
                Id = Guid.NewGuid(),
                Name = request.name,
                Description = request.description,
                ImageUrl = request.imageUrl,
                ParentId = request.parentId,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = "Admin@gmail.com",
                IsDeleted = false
            };
            _categoryRepository.Add(newCategory);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var result = new CreateCategoryResponse
                (newCategory.Id,
                newCategory.Name,
                newCategory.Description,
                newCategory.ParentId,
                newCategory.ImageUrl);
            return Result.Success(result);
        }
    }
}
