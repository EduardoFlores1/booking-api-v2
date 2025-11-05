
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.DataBase.Booking.Queries.GetBookingsByDocumentNumber
{
    public class GetBookingsByDocumentNumberQuery: IGetBookingsByDocumentNumberQuery
    {
        private readonly IDataBaseService _dataBaseService;

        public GetBookingsByDocumentNumberQuery(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public async Task<List<GetBookingsByDocumentNumberModel>> Execute(string documentNumber) {
            var result = await (from booking in _dataBaseService.Bookings
                                join customer in _dataBaseService.Customers
                                on booking.CustomerId equals customer.CustomerId
                                where customer.DocumentNumber == documentNumber
                                select new GetBookingsByDocumentNumberModel { 
                                    RegisterDate = booking.RegisterDate,
                                    Code = booking.Code,
                                    Type = booking.Type
                                }).ToListAsync();
            return result;
        }
    }
}
