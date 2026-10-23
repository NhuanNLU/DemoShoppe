using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Auth.Request;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;
using DemoShoppe.Domain.Entities;

namespace DemoShoppe.Application.UserCase.Commands.Auth
{
    public class RegisterCommandHander : ICommandHandler<RegisterRequest>
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
        public async Task<Result> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var checkEmail = await _userRepository.CheckEmailExistAsync(request.Email, cancellationToken);
            if (checkEmail) return Result.Failure(Error.Conflict("Email da ton tai"));
            var checkUsername = await _userRepository.CheckUsernameExistAsync(request.Username, cancellationToken);
            if (checkUsername) return Result.Failure(Error.Conflict("Username da ton tai"));
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Username = request.Username,
                PasswordHash = request.PasswordHash, //chưa mã hóa
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = request.Email,
                IsDeleted = false
            };
            _userRepository.Add(newUser);
            var newProfile = new Profile
            {
                Id = Guid.NewGuid(),
                UserId = newUser.Id,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = request.Email
            };
            _profileRepository.Add(newProfile);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return Result.Success();
        }
    }
}
