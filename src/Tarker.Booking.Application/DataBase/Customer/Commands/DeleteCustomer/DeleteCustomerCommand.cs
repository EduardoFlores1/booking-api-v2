
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.DataBase.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand: IDeleteCustomerCommand
    {
        private readonly IDataBaseService _databaseService;

        public DeleteCustomerCommand(IDataBaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> Execute(int customerId) {
            var entity = await _databaseService.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (entity == null) return false;

            _databaseService.Customers.Remove(entity);
            return await _databaseService.SaveAsync();
        }
    }
}
