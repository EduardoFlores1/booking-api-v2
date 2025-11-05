
using AutoMapper;
using Tarker.Booking.Domain.Entities.Customer;

namespace Tarker.Booking.Application.DataBase.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand: IUpdateCustomerCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;

        public UpdateCustomerCommand(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<UpdateCustomerModel> Execute(UpdateCustomerModel model) {
            var entity = _mapper.Map<CustomerEntity>(model);
            _dataBaseService.Customers.Update(entity);
            await _dataBaseService.SaveAsync();
            return model;
        }
    }
}
