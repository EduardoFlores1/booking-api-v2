using FluentValidation;

namespace Tarker.Booking.Application.Validations.User
{
    public class GetUserByUserNameAndPasswordValidator: AbstractValidator<(string, string)>
    {
        public GetUserByUserNameAndPasswordValidator()
        {
            RuleFor(u => u.Item1).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(u => u.Item2).NotNull().NotEmpty().MaximumLength(10);
        }
    }
}
