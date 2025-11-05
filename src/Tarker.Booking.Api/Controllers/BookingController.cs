using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.DataBase.Booking.Commands.CreateBooking;
using Tarker.Booking.Application.DataBase.Booking.Queries.GetAllBooking;
using Tarker.Booking.Application.DataBase.Booking.Queries.GetBookingsByDocumentNumber;
using Tarker.Booking.Application.DataBase.Booking.Queries.GetBookingsByType;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class BookingController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateBooking(
            [FromBody] CreateBookingModel model,
            [FromServices] ICreateBookingCommand createBookingCommand,
            [FromServices] IValidator<CreateBookingModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest,
                    validate.Errors));

            var data = await createBookingCommand.Execute(model);

            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(
                StatusCodes.Status201Created, data));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBookings(
            [FromServices] IGetAllBookingQuery getAllBookingQuery)
        {
            var data = await getAllBookingQuery.Execute();

            if (data == null || data.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound));
            }

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-documentNumber")]
        public async Task<IActionResult> GetAllBookings(
            [FromQuery] string documentNumber,
            [FromServices] IGetBookingsByDocumentNumberQuery getBookingsByDocumentNumberQuery)
        {
            if (string.IsNullOrEmpty(documentNumber))
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest));
            }

            var data = await getBookingsByDocumentNumberQuery.Execute(documentNumber);

            if (data == null || data.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound));
            }

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType(
            [FromQuery] string type,
            [FromServices] IGetBookingsByTypeQuery getBookingsByTypeQuery)
        {
            if (string.IsNullOrEmpty(type))
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest));
            }

            var data = await getBookingsByTypeQuery.Execute(type);

            if (data == null || data.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound));
            }

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }
    }
}
