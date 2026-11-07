using BookManagement._3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(255).IsRequired();
            builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Role).HasMaxLength(20).HasDefaultValue("User");
            builder.HasIndex(x => x.Username).IsUnique();
        }
    }
}
