using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Category.Request;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Commands.Category
{
    public sealed class DeleteCategoryByIdCommandHandler :
        ICommandHandler<DeleteCategoryByIdCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryByIdCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository
                .FindCommand().FirstOrDefaultAsync(x => x.Id == request.id && x.IsDeleted == false, cancellationToken);
            if (category is null)
                return Result.Failure(Error.NotFound("Không tìm thấy danh mục cần xóa"));
            var categoryChild = await _categoryRepository
                .FindCommand().FirstOrDefaultAsync(x => x.ParentId == request.id && x.IsDeleted == false, cancellationToken);
            if (categoryChild is not null)
                return Result.Failure(Error.Conflict("Không được xóa danh mục cha khi có danh mục con"));
            category.IsDeleted = true;
            category.UpdatedAt = DateTimeOffset.UtcNow;
            category.UpdatedBy = "Admin@gmail.com";
            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
