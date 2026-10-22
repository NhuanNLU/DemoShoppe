using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Enum;

namespace DemoShoppe.Domain.Entities
{
    public class Profile : EntityAuditSortDeleteBase<int>
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
        public Gender Gender { get; set; } = Gender.Unknown;
        public DateOnly? DateOfBirth { get; set; }
        public string? Bio { get; set; }
        public string? CoverUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public User User { get; set; }
    }
}
