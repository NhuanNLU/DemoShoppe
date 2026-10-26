using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Product.Request;
using DemoShoppe.Contract.Services.Product.Response;
using DemoShoppe.Domain.Abstractions.Repositories;

namespace DemoShoppe.Application.UserCase.Queries.Product
{
    public class GetProductByIdQueryHandler :
        IQueryHandler<GetProductByIdCommand, GetProductByIdResponse>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result<GetProductByIdResponse>> Handle
            (GetProductByIdCommand request, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.FindById(request.id, cancellationToken);
            if (product is null)
                return Result.Failure<GetProductByIdResponse>(Error.NotFound("Không tìm thấy sản phẩm"));
            var result = new GetProductByIdResponse(
                product.Id, product.Name, product.Description,
                product.Price, product.Quantity);
            return Result.Success(result);
        }
    }
}
