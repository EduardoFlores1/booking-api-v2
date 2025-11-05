using FluentValidation;
using Tarker.Booking.Application.DataBase.Customer.Commands.CreateCustomer;

namespace Tarker.Booking.Application.Validations.Customer
{
    public class CreateCustomerValidator: AbstractValidator<CreateCustomerModel>
    {
        public CreateCustomerValidator()
        {
            RuleFor(c => c.FullName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(c => c.DocumentNumber).NotNull().NotEmpty().Length(8);
        }
    }
}
