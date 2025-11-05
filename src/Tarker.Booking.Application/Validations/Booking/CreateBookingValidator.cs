using FluentValidation;
using Tarker.Booking.Application.DataBase.Booking.Commands.CreateBooking;

namespace Tarker.Booking.Application.Validations.Booking
{
    public class CreateBookingValidator: AbstractValidator<CreateBookingModel>
    {
        public CreateBookingValidator()
        {
            RuleFor(b => b.Code).NotNull().NotEmpty().Length(8);
            RuleFor(b => b.Type).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(b => b.CustomerId).NotNull().GreaterThan(0);
            RuleFor(b => b.UserId).NotNull().GreaterThan(0);
        }
    }
}
