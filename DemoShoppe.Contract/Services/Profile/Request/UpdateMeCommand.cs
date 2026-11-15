using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Services.Profile.Response;
using System.Windows.Input;

namespace DemoShoppe.Contract.Services.Profile.Request
{
    public sealed record UpdateMeCommand(
        string? firstName,
        string? lastName,
        string? phoneNumber,
        string? address,
        string? avatarUrl,
        string? gender,
        DateOnly? DateOfBirth,
        string? Bio,
        string? CoverUrl,
        string? facebookUrl): ICommand<UpdateMeResponse>;
    
}
