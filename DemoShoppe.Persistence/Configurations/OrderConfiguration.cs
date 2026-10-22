using DemoShoppe.Domain.Entities;
using DemoShoppe.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoShoppe.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.OrderCode)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Price)
                   .HasColumnType("decimal(18,2)");

            builder.HasIndex(e => e.OrderCode).IsUnique();

            builder.Property(o => o.Status)
                .IsRequired()
                .HasDefaultValue(OrderStatus.Pending);

            builder.HasOne(e => e.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
