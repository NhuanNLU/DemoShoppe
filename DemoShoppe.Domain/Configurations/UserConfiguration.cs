using DemoShoppe.Domain.Entities;
using DemoShoppe.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoShoppe.Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(u => u.PasswordHash)
                   .HasMaxLength(255)
                   .IsRequired();
            builder.Property(u => u.Email)
                   .HasMaxLength(255)
                   .IsRequired();
            builder.Property(u => u.StaffCode)
                   .HasMaxLength(50);
            builder.HasIndex(u => u.Email)
                   .IsUnique();
            builder.HasIndex(u => u.Username)
                   .IsUnique();
            builder.Property(u => u.Status)
                .IsRequired()
                .HasDefaultValue(UserStatus.Inactive);
        }
    }
}