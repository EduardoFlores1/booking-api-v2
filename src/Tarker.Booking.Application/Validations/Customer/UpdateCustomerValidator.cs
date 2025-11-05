using FluentValidation;
using Tarker.Booking.Application.DataBase.Customer.Commands.UpdateCustomer;

namespace Tarker.Booking.Application.Validations.Customer
{
    public class UpdateCustomerValidator: AbstractValidator<UpdateCustomerModel>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(c => c.CustomerId).NotNull().GreaterThan(0);
            RuleFor(c => c.FullName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(c => c.DocumentNumber).NotNull().NotEmpty().Length(8);
        }
    }
}
