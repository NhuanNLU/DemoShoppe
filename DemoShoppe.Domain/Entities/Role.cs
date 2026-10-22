using DemoShoppe.Domain.Abstractions;

namespace DemoShoppe.Domain.Entities
{
    public class Role: EntityAuditSortDeleteBase<int>
    {
        public required string Name { get; set; } 
        public string? Description { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
