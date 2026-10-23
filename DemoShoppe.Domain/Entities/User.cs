using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Enum;

namespace DemoShoppe.Domain.Entities
{
    public class User : EntityAuditSortDeleteBase<Guid>
    {
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Inactive;
        public required string Email { get; set; }
        public bool EmailVerified { get; set; } = false;
        public string? StaffCode { get; set; }

        public Profile? Profile { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
