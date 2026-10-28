using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Product.Request;
using DemoShoppe.Contract.Services.Product.Response;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Queries.Product
{
    public sealed class GetAllProductQueryHandler :
        IQueryHandler<GetAllProductCommand, PageResult<GetAllProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result<PageResult<GetAllProductResponse>>> Handle(GetAllProductCommand request, CancellationToken cancellationToken)
        {
            if (request.pageIndex <= 0 || request.pageSize <= 0)
                return Result.Failure<PageResult<GetAllProductResponse>>(Error.BadRequest("Số trang và số lượng phần tử phải lớn hơn 0"));
            var query = _productRepository.FindQuery();
            var listProduct = await query
                .Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize)
                .Select(p => new GetAllProductResponse
                    (p.Id, p.Name, p.Description, p.Price, p.Quantity))
                .ToListAsync();
            var result = new PageResult<GetAllProductResponse>()
            {
                Items = listProduct,
                PageIndex = request.pageIndex,
                PageSize = request.pageSize,
                TotalItems = await _productRepository.TotalItems(cancellationToken)
            };
            return Result.Success(result);
        }
    }
}
