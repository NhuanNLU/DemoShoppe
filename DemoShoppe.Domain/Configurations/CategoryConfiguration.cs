using DemoShoppe.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DemoShoppe.Domain.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(e => e.ImageUrl)
                   .HasMaxLength(500);
            builder.HasOne(e => e.Parent)
                   .WithMany(c => c.Children)
                   .HasForeignKey(e => e.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
