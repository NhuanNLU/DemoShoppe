using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;

namespace DemoShoppe.Application.UserCase.Commands.Category
{
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryRequest>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.CategoryNameExists(request.Name))
                return Result.Failure(Error.Conflict("Category nay da ton tai"));
            if (request.ParentId is not null)
            {
                if (await _categoryRepository.FindById(request.ParentId.Value) is null)
                    return Result.Failure(Error.NotFound("Khong tim thay category cha"));
            }
            var newCategory = new Domain.Entities.Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                ParentId = request.ParentId,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = "Admin@gmail.com",
                IsDeleted = false
            };
            _categoryRepository.Add(newCategory);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return Result.Success();
        }
    }
}
