using FluentValidation;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUser;

namespace Tarker.Booking.Application.Validations.User
{
    public class UpdateUserValidator: AbstractValidator<UpdateUserModel>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.UserId).NotNull().GreaterThan(0);
            RuleFor(u => u.FirstName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.LastName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Username).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Password).NotNull().NotEmpty().MaximumLength(10);
        }
    }
}
