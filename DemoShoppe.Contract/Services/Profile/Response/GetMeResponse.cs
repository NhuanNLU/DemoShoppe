namespace DemoShoppe.Contract.Services.Profile.Response
{
    public sealed record GetMeResponse(
        Guid userId,
        string? firstName,
        string? lastName,
        string? phoneNumber,
        string? address,
        string? avatarUrl,
        string? gender,    
        DateOnly? DateOfBirth,
        string? Bio,
        string? CoverUrl,
        string? facebookUrl);
}
