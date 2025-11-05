
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.DataBase.Booking.Queries.GetAllBooking
{
    public class GetAllBookingQuery: IGetAllBookingQuery
    {
        private readonly IDataBaseService _dataBaseService;
        public GetAllBookingQuery(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public async Task<List<GetAllBookingModel>> Execute() {
            var result = await (from booking in _dataBaseService.Bookings
                                join customer in _dataBaseService.Customers
                                on booking.CustomerId equals customer.CustomerId
                                select new GetAllBookingModel { 
                                    BookingId =  booking.BookingId,
                                    RegisterDate = booking.RegisterDate,
                                    Code = booking.Code,
                                    Type = booking.Type,
                                    CustomerFullName = customer.FullName,
                                    CustomerDocumentNumber = customer.DocumentNumber
                                }).ToListAsync();

            return result;
        }
    }
}
