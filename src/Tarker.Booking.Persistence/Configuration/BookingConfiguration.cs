
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarker.Booking.Domain.Entities.Booking;

namespace Tarker.Booking.Persistence.Configuration
{
    public class BookingConfiguration
    {
        public BookingConfiguration(EntityTypeBuilder<BookingEntity> entityBuilder)
        {
            entityBuilder.ToTable("Booking");
            entityBuilder.HasKey(b => b.BookingId);
            entityBuilder.Property(b => b.RegisterDate).IsRequired();
            entityBuilder.Property(b => b.Code).IsRequired();
            entityBuilder.Property(b => b.Type).IsRequired();
            entityBuilder.Property(b => b.CustomerId).IsRequired();
            entityBuilder.Property(b => b.UserId).IsRequired();

            entityBuilder.HasOne(b => b.UserReference)
                .WithMany(u => u.BookingsReference)
                .HasForeignKey(b => b.UserId);

            entityBuilder.HasOne(b => b.CustomerReference)
                .WithMany(c => c.BookingsReference)
                .HasForeignKey(b => b.CustomerId);
        }
    }
}
