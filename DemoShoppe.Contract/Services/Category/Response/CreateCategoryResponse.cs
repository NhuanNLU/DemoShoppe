namespace DemoShoppe.Contract.Services.Category.Response
{
    public sealed record CreateCategoryResponse
        (Guid id,
        string name,
        string? description,
        Guid? parentId,
        string? imageUrl);
}
