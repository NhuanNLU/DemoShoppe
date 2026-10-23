using DemoShoppe.Contract.Abstractions.Message;

namespace DemoShoppe.Contract.Services.Category.Request
{
    public class UpdateCategoryByIdRequest : ICommand
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
