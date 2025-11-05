
using AutoMapper;

namespace Tarker.Booking.Application.DataBase.Booking.Commands.CreateBooking
{
    public class CreateBookingCommand: ICreateBookingCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public CreateBookingCommand(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<CreateBookingModel> Execute(CreateBookingModel model) { 
            var entity = _mapper.Map<Domain.Entities.Booking.BookingEntity>(model);
            entity.RegisterDate = DateTime.Now;
            await _dataBaseService.Bookings.AddAsync(entity);
            await _dataBaseService.SaveAsync();
            return model;
        }
    }
}
