using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Product.Request;
using DemoShoppe.Contract.Services.Product.Response;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Queries.Product
{
    public sealed class GetMyProductsQueryHandler : IQueryHandler<GetMyProductsQuery, PageResult<GetMyProductsResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetMyProductsQueryHandler(IProductRepository productRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Result<PageResult<GetMyProductsResponse>>> Handle(GetMyProductsQuery request, CancellationToken cancellationToken)
        {
            var uId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            var userId = Guid.Parse(uId!);
            var products = _productRepository.FindQuery().Where(x => x.UserId == userId && x.IsDeleted == false);
            var pageProducts = products.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize);
            var productsDto = pageProducts.Select(x => new GetMyProductsResponse(x.Id, x.Name, x.Description, x.Price, x.Quantity));
            var result = new PageResult<GetMyProductsResponse>()
            {
                Items = await productsDto.ToListAsync(cancellationToken),
                PageIndex = request.pageIndex,
                PageSize = request.pageSize,
                TotalItems = await _productRepository.TotalItems()
            };
            return Result.Success(result);
        }
    }
}
