using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Profile.Request;
using DemoShoppe.Contract.Services.Profile.Response;
using DemoShoppe.Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Application.UserCase.Queries.Profile
{
    public sealed class GetMeQueryHandler : 
        IQueryHandler<GetMeQuery, GetMeResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetMeQueryHandler(IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IProfileRepository profileRepository)

        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _profileRepository = profileRepository;
        }
        public async Task<Result<GetMeResponse>> Handle(GetMeQuery request, CancellationToken cancellationToken)
        {
            var uId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            if (string.IsNullOrWhiteSpace(uId))
                return Result.Failure<GetMeResponse>(Error.NotFound("Không tìm thấy user"));
            var userId = Guid.Parse(uId!);
            var user = await _userRepository.FindQuery()
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            if(user is null)
                return Result.Failure<GetMeResponse>(Error.NotFound("Không tìm thấy user"));
            var profile = await _profileRepository.FindQuery()
                .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
            if(profile is null)
                return Result.Failure<GetMeResponse>(Error.NotFound("Không tìm thấy profile"));
            var result = new GetMeResponse(
                profile.UserId,
                profile.FirstName,
                profile.LastName,
                profile.PhoneNumber,
                profile.Address,
                profile.AvatarUrl,
                profile.Gender.ToString(),
                profile.DateOfBirth,
                profile.Bio,
                profile.CoverUrl,
                profile.FacebookUrl);
            return Result.Success(result);
        }
    }
}
