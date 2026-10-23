using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;

namespace DemoShoppe.Application.UserCase.Commands.Category
{
    public class UpdateCategoryByIdCommandHandler : ICommandHandler<UpdateCategoryByIdRequest>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCategoryByIdCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindById(request.Id);
            if (category is null) return Result.Failure(Error.NotFound("Khong tim thay category"));
            if (request.Name is not null &&
                !await _categoryRepository.CategoryNameExists(request.Name))
            {
                category.Name = request.Name;
            }
            if (request.Description is not null)
            {
                category.Description = request.Description;
            }
            if (category.ImageUrl is not null)
            {
                category.ImageUrl = request.ImageUrl;
            }
            category.UpdatedAt = DateTimeOffset.UtcNow;
            category.CreatedBy = "Admin@gmail.com";
            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return Result.Success();
        }
    }
}
