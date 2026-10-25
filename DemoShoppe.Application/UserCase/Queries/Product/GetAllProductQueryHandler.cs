using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Product.Request;
using DemoShoppe.Contract.Services.Product.Response;
using DemoShoppe.Domain.Abstractions.Repositories;

namespace DemoShoppe.Application.UserCase.Queries.Product
{
    public class GetAllProductQueryHandler :
        IQueryHandler<GetAllProductCommand, PageResult<GetAllProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result<PageResult<GetAllProductResponse>>> Handle(GetAllProductCommand request, CancellationToken cancellationToken)
        {
            var listProduct = await _productRepository.FindAll(cancellationToken);
            var pageProduct = listProduct.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize);
            var listProductDto = pageProduct.Select(p =>
                new GetAllProductResponse
                (p.Id, p.Name, p.Description, p.Price, p.Quantity)
            ).ToList();
            var result = new PageResult<GetAllProductResponse>()
            {
                Items = listProductDto,
                PageIndex = request.pageIndex,
                PageSize = request.pageSize,
                TotalItems = await _productRepository.TotalItems(cancellationToken)
            };
            return Result.Success(result);
        }
    }
}
