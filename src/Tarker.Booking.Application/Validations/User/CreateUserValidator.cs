using FluentValidation;
using Tarker.Booking.Application.DataBase.User.Commands.CreateUser;

namespace Tarker.Booking.Application.Validations.User
{
    public class CreateUserValidator: AbstractValidator<CreateUserModel>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.FirstName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.LastName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Username).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Password).NotNull().NotEmpty().MaximumLength(10);
        }
    }
}
