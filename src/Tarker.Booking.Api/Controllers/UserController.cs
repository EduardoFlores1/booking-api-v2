using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tarker.Booking.Application.DataBase.User.Commands.CreateUser;
using Tarker.Booking.Application.DataBase.User.Commands.DeleteUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUserPassword;
using Tarker.Booking.Application.DataBase.User.Queries.GetAllUser;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserById;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserByUserNameAndPassword;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UserController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(
            [FromBody] CreateUserModel model,
            [FromServices] ICreateUserCommand createUserCommand,
            [FromServices] IValidator<CreateUserModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest, validate.Errors));
            }

            var data = await createUserCommand.Execute(model);

            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(
                StatusCodes.Status201Created, data));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(
            [FromBody] UpdateUserModel model,
            [FromServices] IUpdateUserCommand updateUserCommand,
            [FromServices] IValidator<UpdateUserModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest, validate.Errors));
            }

            var data = await updateUserCommand.Execute(model);

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdateUserPassword(
            [FromBody] UpdateUserPasswordModel model,
            [FromServices] IUpdateUserPasswordCommand updateUserPasswordCommand,
            [FromServices] IValidator<UpdateUserPasswordModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest, validate.Errors));
            }

            var data = await updateUserPasswordCommand.Execute(model);

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(
            int userId,
            [FromServices] IDeleteUserCommand deleteUserCommand)
        {
            if (userId <= 0) 
            return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                StatusCodes.Status400BadRequest));

            var data = await deleteUserCommand.Execute(userId);

            if (!data)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound, data));
            }

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers(
            [FromServices] IGetAllUserQuery getAllUserQuery)
        {
            var data = await getAllUserQuery.ExecuteAsync();

            if (data == null || data.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound, data));
            }

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-id/{userId}")]
        public async Task<IActionResult> GetByIdUser(
            int userId,
            [FromServices] IGetUserByIdQuery getUserByIdQuery)
        {
            var data = await getUserByIdQuery.Execute(userId);

            if (userId <= 0)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest));

            if (data == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound, data));
            }

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-username-password/{username}/{password}")]
        public async Task<IActionResult> GetByUserNameAndPassword(
            string username,
            string password,
            [FromServices] IGetUserByUserNameAndPasswordQuery getUserByUserNameAndPasswordQuery,
            [FromServices] IValidator<(string, string)> validator)
        {
            var validate = await validator.ValidateAsync((username, password));

            if (!validate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest, validate.Errors));
            }

            var data = await getUserByUserNameAndPasswordQuery.Execute(username, password);

            if (data == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound, data));
            }

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK, data));
        }

    }
}
