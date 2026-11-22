using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Cart.Request;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Commands.Cart
{
    public sealed class AddToCartCommandHandler : ICommandHandler<AddToCartCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public AddToCartCommandHandler(IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork, ICartRepository cartRepository, IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var uId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            if (string.IsNullOrEmpty(uId))
            {
                return Result.Failure(Error.NotFound("Không tìm thấy người dùng này"));
            }
            if (!Guid.TryParse(uId, out var userId))
            {
                return Result.Failure(Error.NotFound("UserId không hợp lệ"));
            }
            var product = await _productRepository.FindQuery()
                .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
            if (product is null)
            {
                return Result.Failure(Error.NotFound("Không tìm thấy sản phẩm này"));
            }
            if (request.Quantity <= 0)
            {
                return Result.Failure(Error.BadRequest("Số lượng sản phẩm phải lớn hơn 0"));
            }
            var cart = await _cartRepository.FindQuery()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == request.ProductId, cancellationToken);
            if (request.Quantity > product.Quantity)
            {
                return Result.Failure(Error.BadRequest("Số lượng sản phẩm trong kho không đủ"));
            }
            if (cart is not null)
            {
                if (request.Quantity + cart.Quantity > product.Quantity)
                {
                    return Result.Failure(Error.BadRequest("Số lượng sản phẩm trong kho không đủ"));
                }
                cart.Quantity += request.Quantity;
                cart.UpdatedAt = DateTime.UtcNow;
                cart.UpdatedBy = userId.ToString();
                _cartRepository.Update(cart);
            }
            else
            {
                var newCart = new Domain.Entities.Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId.ToString(),
                };
                _cartRepository.Add(newCart);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
