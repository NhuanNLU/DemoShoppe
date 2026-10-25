namespace DemoShoppe.Contract.Services.Category.Response
{
    public sealed record UpdateCategoryByIdResponse(
        Guid id,
        string name,
        string? description,
        string? imageUrl);
}
