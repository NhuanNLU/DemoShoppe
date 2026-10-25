namespace DemoShoppe.Contract.Services.Category.Response
{
    public sealed record GetCategoryByIdResponse(
        Guid id,
        string name,
        string? description,
        string? image);
}
