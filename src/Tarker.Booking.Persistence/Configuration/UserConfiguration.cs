
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Persistence.Configuration
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<UserEntity> entityBuilder)
        {
            entityBuilder.ToTable("User");
            entityBuilder.HasKey(u => u.UserId);
            entityBuilder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            entityBuilder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            entityBuilder.Property(u => u.Username).IsRequired().HasMaxLength(50);
            entityBuilder.Property(u => u.Password).IsRequired().HasMaxLength(10);

            entityBuilder.HasMany(u => u.BookingsReference)
                .WithOne(b => b.UserReference)
                .HasForeignKey(b => b.UserId);
        }
    }
}
