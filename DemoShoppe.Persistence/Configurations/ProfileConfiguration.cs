using DemoShoppe.Domain.Entities;
using DemoShoppe.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DemoShoppe.Persistence.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FirstName)
                .HasMaxLength(50);
            builder.Property(p => p.LastName)
                .HasMaxLength(50);
            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(10);
            builder.Property(p => p.Address)
                .HasMaxLength(255);
            builder.Property(p => p.AvatarUrl)
                .HasMaxLength(255);
            builder.Property(p => p.Gender)
                .HasDefaultValue(Gender.Unknown);
            builder.Property(p => p.CoverUrl)
                .HasMaxLength(255);
            builder.Property(p => p.FacebookUrl)
                .HasMaxLength(255);
            builder.HasOne(p => p.User)
                .WithOne(p => p.Profile)
                .HasForeignKey<Profile>(p => p.UserId);
        }
    }
}
