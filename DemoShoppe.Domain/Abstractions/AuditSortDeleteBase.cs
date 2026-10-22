using DemoShoppe.Domain.Abstractions.Entities;

namespace DemoShoppe.Domain.Abstractions
{
    public abstract class AuditSortDeleteBase :
        IAuditBase, ISortDelete
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public required string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
