namespace DemoShoppe.Contract.Services.Category.Response
{
    public sealed record GetAllCategoryResponse(
        Guid id,
        string name,
        string? imageUrl,
        string? description,
        Guid? parentId);
}
