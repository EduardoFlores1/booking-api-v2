
using Microsoft.EntityFrameworkCore;
using Tarker.Booking.Domain.Entities.Booking;
using Tarker.Booking.Domain.Entities.Customer;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Application.DataBase
{
    public interface IDataBaseService
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<CustomerEntity> Customers { get; set; }
        DbSet<BookingEntity> Bookings { get; set; }
        Task<bool> SaveAsync();
    }
}
