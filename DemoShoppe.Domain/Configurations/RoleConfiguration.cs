using DemoShoppe.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoShoppe.Domain.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(r => r.Description)
                .HasMaxLength(250);
        }
    }
}
