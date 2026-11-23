using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Cart.Request;
using DemoShoppe.Contract.Services.Cart.Response;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace DemoShoppe.Application.UserCase.Queries.Cart
{
    public sealed class GetMyCartQueryHandler : IQueryHandler<GetMyCartQuery, GetMyCartResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        public GetMyCartQueryHandler(IHttpContextAccessor httpContextAccessor,
            ICartRepository cartRepository, IProductRepository productRepository,
            IProductImageRepository productImageRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
        }
        public async Task<Result<GetMyCartResponse>> Handle(GetMyCartQuery request, CancellationToken cancellationToken)
        {
            var uId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            if (string.IsNullOrEmpty(uId))
            {
                return Result.Failure<GetMyCartResponse>(Error.NotFound("Không tìm thấy người dùng này"));
            }
            if (!Guid.TryParse(uId, out var userId))
            {
                return Result.Failure<GetMyCartResponse>(Error.NotFound("UserId không hợp lệ"));
            }
            throw new NotImplementedException();
        }
    }
}
