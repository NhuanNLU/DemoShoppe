using DemoShoppe.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DemoShoppe.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(e => e.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(e => e.User)
                   .WithMany(u => u.Products)
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
