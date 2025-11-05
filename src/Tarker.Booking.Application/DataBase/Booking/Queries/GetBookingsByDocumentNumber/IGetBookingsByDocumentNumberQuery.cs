
namespace Tarker.Booking.Application.DataBase.Booking.Queries.GetBookingsByDocumentNumber
{
    public interface IGetBookingsByDocumentNumberQuery
    {
        Task<List<GetBookingsByDocumentNumberModel>> Execute(string documentNumber);
    }
}
