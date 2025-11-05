using Tarker.Booking.Domain.Models.SendGridEmail;

namespace Tarker.Booking.Application.External.SendGridEmail
{
    public interface ISendGridEmailService
    {
        Task<bool> Execute(SendGridEmailRequestModel model);
    }
}
