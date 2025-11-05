
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.DataBase.Customer.Commands.CreateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.DeleteCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.UpdateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetAllCustomer;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustomerByDocumentNumber;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustomerById;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/customers")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class CustomerController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer(
            [FromBody] CreateCustomerModel model,
            [FromServices] ICreateCustomerCommand createCustomerCommand,
            [FromServices] IValidator<CreateCustomerModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest,
                    validate.Errors));

            var data = await createCustomerCommand.Execute(model);

            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(
                StatusCodes.Status201Created, data));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomer(
            [FromBody] UpdateCustomerModel model,
            [FromServices] IUpdateCustomerCommand updateCustomerCommand,
            [FromServices] IValidator<UpdateCustomerModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest,
                    validate.Errors));

            var data = await updateCustomerCommand.Execute(model);

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

        [HttpDelete("delete/{customerId}")]
        public async Task<IActionResult> DeleteCustomer(
            int customerId,
            [FromServices] IDeleteCustomerCommand deleteCustomerCommand)
        {
            if (customerId <= 0)
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await deleteCustomerCommand.Execute(customerId);

            if (!data)
                return StatusCode(StatusCodes.Status404NotFound,
                    ResponseApiService.Response(StatusCodes.Status404NotFound, data));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCustomers(
            [FromServices] IGetAllCustomerQuery getAllCustomerQuery)
        {
            var data = await getAllCustomerQuery.Execute();

            if (data == null || data.Count == 0)
                return StatusCode(StatusCodes.Status204NoContent,
                    ResponseApiService.Response(StatusCodes.Status204NoContent));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-id/{customerId}")]
        public async Task<IActionResult> GetByIdCustomer(
            int customerId,
            [FromServices] IGetCustomerByIdQuery getCustomerByIdQuery)
        {

            if (customerId <= 0)
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await getCustomerByIdQuery.Execute(customerId);

            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound,
                    ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-documentNumber/{documentNumber}")]
        public async Task<IActionResult> GetByDocumentNumber(
            string documentNumber,
            [FromServices] IGetCustomerByDocumentNumberQuery getCustomerByDocumentNumberQuery)
        {

            if (string.IsNullOrEmpty(documentNumber))
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await getCustomerByDocumentNumberQuery.Execute(documentNumber);

            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound,
                    ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }
    }
}
