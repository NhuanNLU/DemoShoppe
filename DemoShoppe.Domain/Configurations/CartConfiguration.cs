using DemoShoppe.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoShoppe.Domain.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.User)
                   .WithMany(u => u.Carts)
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Product)
                   .WithMany(p => p.Carts)
                   .HasForeignKey(e => e.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
