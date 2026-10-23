using DemoShoppe.Contract.Abstractions.Message;
using System.ComponentModel.DataAnnotations;

namespace DemoShoppe.Contract.Services.Category.Request
{
    public class CreateCategoryRequest : ICommand
    {
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
