using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Profile.Request;
using DemoShoppe.Contract.Services.Profile.Response;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;
using DemoShoppe.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Commands.Profile
{
    public sealed class UpdateMeCommandHandler :
        ICommandHandler<UpdateMeCommand, UpdateMeResponse>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateMeCommandHandler(IProfileRepository profileRepository,
            IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<UpdateMeResponse>> Handle(UpdateMeCommand request, CancellationToken cancellationToken)
        {
            var uId = _httpContextAccessor.HttpContext!.User.Claims
                .FirstOrDefault(x => x.Type == "UserId")?.Value;
            var userId = Guid.Parse(uId!);
            var profile = await _profileRepository.FindCommand()
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
            if (profile is null)
                return Result.Failure<UpdateMeResponse>(Error.NotFound("không tìm thấy profile"));
            if (!string.IsNullOrWhiteSpace(request.firstName))
            {
                profile.FirstName = request.firstName;
            }
            if (!string.IsNullOrWhiteSpace(request.lastName))
            {
                profile.LastName = request.lastName;
            }
            if (!string.IsNullOrWhiteSpace(request.phoneNumber))
            {
                profile.PhoneNumber = request.phoneNumber;
            }
            if (request.address is not null)
            {
                profile.Address = request.address;
            }
            if (!string.IsNullOrWhiteSpace(request.avatarUrl))
            {
                profile.AvatarUrl = request.avatarUrl;
            }
            if (!string.IsNullOrWhiteSpace(request.gender))
            {
                if (request.gender.Equals(Gender.Male.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    profile.Gender = Gender.Male;

                }
                else if (request.gender.Equals(Gender.Female.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    profile.Gender = Gender.Female;
                }
                else if (request.gender.Equals(Gender.Other.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    profile.Gender = Gender.Other;
                }
                else
                {
                    profile.Gender = Gender.Unknown;
                }
            }
            if (request.DateOfBirth.HasValue)
            {
                profile.DateOfBirth = request.DateOfBirth.Value;
            }
            if (request.Bio is not null)
            {
                profile.Bio = request.Bio;
            }
            if (!string.IsNullOrWhiteSpace(request.CoverUrl))
            {
                profile.CoverUrl = request.CoverUrl;
            }
            if (!string.IsNullOrWhiteSpace(request.facebookUrl))
            {
                profile.FacebookUrl = request.facebookUrl;
            }
            profile.UpdatedAt = DateTimeOffset.UtcNow;
            profile.UpdatedBy = userId.ToString();
            _profileRepository.Update(profile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var result = new UpdateMeResponse(profile.FirstName,
                profile.LastName, profile.PhoneNumber, profile.Address,
                profile.AvatarUrl, profile.Gender.ToString(), profile.DateOfBirth,
                profile.Bio, profile.CoverUrl, profile.FacebookUrl);
            return Result.Success(result);
        }
    }
}
