using DemoShoppe.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DemoShoppe.Domain.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(e => new { e.ProductId, e.CategoryId });

            builder.HasOne(e => e.Product)
                   .WithMany(p => p.ProductCategories)
                   .HasForeignKey(e => e.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Category)
                   .WithMany(c => c.ProductCategories)
                   .HasForeignKey(e => e.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
