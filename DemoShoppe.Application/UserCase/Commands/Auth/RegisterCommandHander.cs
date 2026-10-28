using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Auth.Request;
using DemoShoppe.Contract.Services.Auth.Response;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;
using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Application.UserCase.Commands.Auth
{
    public sealed class RegisterCommandHander : ICommandHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCommandHander(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IProfileRepository profileRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _profileRepository = profileRepository;
        }

        public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var checkEmail = await _userRepository.CheckEmailExistAsync(request.email, cancellationToken);
            if (checkEmail) return Result.Failure<RegisterResponse>(Error.Conflict("Email đã tồn tại"));
            var checkUsername = await _userRepository.CheckUsernameExistAsync(request.username, cancellationToken);
            if (checkUsername) return Result.Failure<RegisterResponse>(Error.Conflict("Username đã tồn tại"));
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = request.email,
                Username = request.username,
                PasswordHash = request.passwordHash, //chưa mã hóa
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = request.email,
                IsDeleted = false
            };
            _userRepository.Add(newUser);
            var newProfile = new Profile
            {
                Id = Guid.NewGuid(),
                UserId = newUser.Id,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = request.email
            };
            _profileRepository.Add(newProfile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(new RegisterResponse(newUser.Email, newUser.Username));
        }
    }
}
