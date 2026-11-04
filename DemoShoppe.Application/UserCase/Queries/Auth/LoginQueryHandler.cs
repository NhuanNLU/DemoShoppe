using DemoShoppe.Application.Abstractions;
using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Auth.Request;
using DemoShoppe.Contract.Services.Auth.Response;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DemoShoppe.Application.UserCase.Queries.Auth
{
    public sealed class LoginQueryHandler : 
        ICommandHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var isEmailExist = await _userRepository.CheckEmailExistAsync(request.email, cancellationToken);
            if (isEmailExist is false)
            {
                return Result.Failure<LoginResponse>(Error.NotFound("Email không tồn tại"));
            }
            var user = await _userRepository.FindQuery()
                .FirstOrDefaultAsync(x => x.Email == request.email && 
                                            x.PasswordHash == request.password && 
                                            x.IsDeleted == false, cancellationToken);
            if (user is null)
                return Result.Failure<LoginResponse>(Error.BadRequest("Email hoặc password không hợp lệ"));
            var claims = new List<Claim> { 
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Username", user.Username)
            };
            var accessToken = _jwtTokenService.GenerateAccessToken(claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();
            var result = new LoginResponse(accessToken, refreshToken);
            return Result.Success(result);
        }
    }
}
