using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Cart.Request;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Commands.Cart
{
    public sealed class RemoveProductsFromCartCommandHandler : ICommandHandler<RemoveProductsFromCartCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartRepository _cartRepository;
        public RemoveProductsFromCartCommandHandler(IHttpContextAccessor httpContextAccessor,
            ICartRepository cartRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartRepository = cartRepository;
        }
        public async Task<Result> Handle(RemoveProductsFromCartCommand request, CancellationToken cancellationToken)
        {
            var uId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            if (string.IsNullOrEmpty(uId))
            {
                return Result.Failure(Error.BadRequest("Không tìm thấy người dùng này"));
            }
            if (!Guid.TryParse(uId, out var userId))
            {
                return Result.Failure(Error.BadRequest("UserId không hợp lệ"));
            }
            if (request.productIds is null || request.productIds.Count == 0)
            {
                return Result.Failure(Error.BadRequest("Danh sách sản phẩm không được để trống"));
            }
            await _cartRepository.FindCommand()
                .Where(x => x.UserId == userId && !x.IsDeleted && request.productIds.Contains(x.ProductId))
                .ExecuteUpdateAsync(x => x.SetProperty(c => c.IsDeleted, true)
                                            .SetProperty(c => c.UpdatedAt, DateTimeOffset.UtcNow)
                                            .SetProperty(c => c.UpdatedBy, userId.ToString()), cancellationToken);
            return Result.Success();
        }
    }
}
