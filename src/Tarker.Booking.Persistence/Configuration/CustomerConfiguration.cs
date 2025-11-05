
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarker.Booking.Domain.Entities.Customer;

namespace Tarker.Booking.Persistence.Configuration
{
    public class CustomerConfiguration
    {
        public CustomerConfiguration(EntityTypeBuilder<CustomerEntity> entityBuilder)
        {
            entityBuilder.ToTable("Customer");
            entityBuilder.HasKey(c => c.CustomerId);
            entityBuilder.Property(c => c.FullName).IsRequired().HasMaxLength(50);
            entityBuilder.Property(c => c.DocumentNumber).IsRequired().HasMaxLength(8);

            entityBuilder.HasMany(c => c.BookingsReference)
                .WithOne(b => b.CustomerReference)
                .HasForeignKey(b => b.CustomerId);
        }
    }
}
