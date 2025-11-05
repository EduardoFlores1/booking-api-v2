
namespace Tarker.Booking.Application.DataBase.Booking.Commands.CreateBooking
{
    public class CreateBookingModel
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
    }
}
