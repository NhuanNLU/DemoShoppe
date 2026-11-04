namespace DemoShoppe.Contract.Services.Auth.Response
{
    public sealed record LoginResponse
    (string accessToken, string refreshToken);
}
