using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Product.Request;
using DemoShoppe.Contract.Services.Product.Response;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;

namespace DemoShoppe.Application.UserCase.Commands.Product
{
    public sealed class UpdateProductByIdCommandHandler :
        ICommandHandler<UpdateProductByIdCommand, UpdateProductByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        public UpdateProductByIdCommandHandler(
            IUnitOfWork unitOfWork,
            IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }
        public async Task<Result<UpdateProductByIdResponse>> Handle(UpdateProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindById(request.id, cancellationToken);
            if (product is null)
                return Result.Failure<UpdateProductByIdResponse>
                    (Error.NotFound("Sản phẩm không tồn tại"));
            if (!string.IsNullOrWhiteSpace(request.name))
            {
                product.Name = request.name;
            }
            if (!string.IsNullOrWhiteSpace(request.description))
            {
                product.Description = request.description;
            }
            if (request.quantity is not null)
            {
                product.Quantity = request.quantity ?? product.Quantity;
            }
            if (request.price is not null)
            {
                product.Price = request.price ?? product.Price;
            }
            product.UpdatedAt = DateTimeOffset.UtcNow;
            product.UpdatedBy = "Admin@gmail.com";
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var result = new UpdateProductByIdResponse(
                product.Id, product.Name, product.Description,
                product.Price, product.Quantity);
            return Result.Success(result);
        }
    }
}
