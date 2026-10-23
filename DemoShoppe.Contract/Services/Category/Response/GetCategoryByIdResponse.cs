namespace DemoShoppe.Contract.Services.Category.Response
{
    public class GetCategoryByIdResponse
    {
        public Guid Id { get; set; }
        public required string name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public Guid? ParentId { get; set; }
    }
}
