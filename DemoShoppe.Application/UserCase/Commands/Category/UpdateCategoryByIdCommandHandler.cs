using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Contract.Services.Category.Response;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;

namespace DemoShoppe.Application.UserCase.Commands.Category
{
    public class UpdateCategoryByIdCommandHandler : ICommandHandler<UpdateCategoryByIdCommand, UpdateCategoryByIdResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCategoryByIdCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UpdateCategoryByIdResponse>> Handle(UpdateCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindById(request.id);
            if (category is null) return Result.Failure<UpdateCategoryByIdResponse>(Error.NotFound("Khong tim thay category"));
            if (request.name is not null &&
                !await _categoryRepository.CategoryNameExists(request.name))
            {
                category.Name = request.name;
            }
            if (request.description is not null)
            {
                category.Description = request.description;
            }
            if (category.ImageUrl is not null)
            {
                category.ImageUrl = request.imageUrl;
            }
            category.UpdatedAt = DateTimeOffset.UtcNow;
            category.CreatedBy = "Admin@gmail.com";
            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var result = new UpdateCategoryByIdResponse(
                category.Id,
                category.Name,
                category.Description,
                category.ImageUrl
                );
            return Result.Success(result);
        }
    }
}
