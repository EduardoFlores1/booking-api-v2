using FluentValidation;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUserPassword;

namespace Tarker.Booking.Application.Validations.User
{
    public class UpdateUserPasswordValidator: AbstractValidator<UpdateUserPasswordModel>
    {
        public UpdateUserPasswordValidator()
        {
            RuleFor(u => u.UserId).NotNull().GreaterThan(0);
            RuleFor(u => u.Password).NotNull().NotEmpty().MaximumLength(10);
        }
    }
}
