using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Contract.Services.Category.Response;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;

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
                return Result.Failure<CreateCategoryResponse>(Error.Conflict("Category nay da ton tai"));
            if (request.parentId is not null)
            {
                if (await _categoryRepository.FindById(request.parentId.Value) is null)
                    return Result.Failure<CreateCategoryResponse>(Error.NotFound("Khong tim thay category cha"));
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
