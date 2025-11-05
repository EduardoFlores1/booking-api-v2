using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.External.SendGridEmail;
using Tarker.Booking.Application.Features;
using Tarker.Booking.Domain.Models.SendGridEmail;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/notifications")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class NotificationController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] SendGridEmailRequestModel model,
            [FromServices] ISendGridEmailService sendGridEmailService)
        { 
            var data = await sendGridEmailService.Execute(model);

            if (!data)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseApiService.Response(StatusCodes.Status500InternalServerError));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }
    }
}
