using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Product.Request;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Commands.Product
{
    public sealed class DeleteProductByIdCommandHandler : ICommandHandler<DeleteProductByIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        public DeleteProductByIdCommandHandler(
            IUnitOfWork unitOfWork,
            IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindCommand()
                .FirstOrDefaultAsync(x => x.Id == request.id && x.IsDeleted == false, cancellationToken);
            if (product is null)
                return Result.Failure(Error.NotFound("Không tìm thấy sản phẩm"));
            product.IsDeleted = true;
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
