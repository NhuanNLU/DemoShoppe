namespace DemoShoppe.Contract.Services.Category.Response
{
    public class GetAllCategoryResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
