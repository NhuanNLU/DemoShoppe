using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Cart.Request;
using DemoShoppe.Contract.Services.Cart.Response;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Queries.Cart
{
    public sealed class GetMyCartQueryHandler : IQueryHandler<GetMyCartQuery, List<GetMyCartResponse>>
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
        public async Task<Result<List<GetMyCartResponse>>> Handle(GetMyCartQuery request, CancellationToken cancellationToken)
        {
            var uId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            if (string.IsNullOrEmpty(uId))
            {
                return Result.Failure<List<GetMyCartResponse>>(Error.NotFound("Không tìm thấy người dùng này"));
            }
            if (!Guid.TryParse(uId, out var userId))
            {
                return Result.Failure<List<GetMyCartResponse>>(Error.NotFound("UserId không hợp lệ"));
            }

            var listCart = await _cartRepository.FindQuery()
                .Where(x => x.UserId == userId && !x.IsDeleted)
                .Select(x => new GetMyCartResponse
                (x.Id, x.UserId, x.ProductId, x.Quantity, x.Product.Price,
                x.Product.ProductImages.Where(pi => pi.IsPrimary)
                                        .Select(pi => pi.ImageUrl)
                                        .FirstOrDefault()))
                .ToListAsync(cancellationToken);

            return Result.Success(listCart);
        }
    }
}
