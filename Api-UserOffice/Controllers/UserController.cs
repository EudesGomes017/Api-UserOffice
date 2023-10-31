using Api_UserOffice.filter.api;
using Domain.Dto;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_UserOffice.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGetUser _getUser1;

        public UserController(IGetUser getUser)
        {
            _getUser1 = getUser;
        }

        //[Authorize(Roles = "Administrador")]
        [HttpPost(Name = "Add User")]
        public async Task<IActionResult> Post([FromServices] IPostUser userService, UserDto model)
        {

            var user = await userService.AddUserAsync(model);
            if (user.IsActive == true)
            {
                return this.StatusCode(StatusCodes.Status201Created, user);
            }

            throw new ErroValidatorException(new List<string> { "Coloborador Disabled" });

        }

        // [Authorize(Roles = "Administrador")]
        [HttpGet(Name = "Search All Users")]
        public async Task<IActionResult> Get([FromServices] IGetUser userService)
        {
            var users = await userService.SearchAllUsersAsync();
            return this.StatusCode(StatusCodes.Status200OK, users);
        }

        // [Authorize(Roles = "Administrador")]
        [HttpGet("{id}", Name = "Search All Users ID")]
        public async Task<IActionResult> Get([FromServices] IGetUser userService, int id)
        {
            var user = await userService.SearchUserIdAsync(id);
            return this.StatusCode(StatusCodes.Status200OK, user);
        }

        //  [Authorize(Roles = "Administrador")]
        [HttpGet("Search/{email}", Name = "Search All Users E-mail")]
        public async Task<IActionResult> GetEmail([FromServices] ISearchEamil userService, string email)
        {

            var user = await userService.BuscaEamil(email);
            return this.StatusCode(StatusCodes.Status200OK, user);

        }

        //[Authorize(Roles = "Administrador")]
        [HttpPut("{id}", Name = "Update User")]
        public async Task<IActionResult> Put([FromServices] IUserUp userService, UserDto modelUser)
        {
            var updatedUser = await userService.UpUserAsync(modelUser);

            if (updatedUser != null && updatedUser.Id == modelUser.Id) // Verifique se o usuário foi atualizado e o ID corresponde
            {
                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            else
            {
                throw new Exception("Error Updating User");
            }
        }

        [HttpPut("UpdateStatus/{email}", Name = "Update Status")]
        public async Task<IActionResult> PutStatus([FromServices] IUserUp userService, string email)
        {
            var updatedUser = await userService.IsActiveUserAsync(email);


            return this.StatusCode(StatusCodes.Status200OK, updatedUser);
        }

        [HttpPut("UpPassword/{id}", Name = "Update Password")]
        [ServiceFilter(typeof(UserAuthentication))]
        public async Task<IActionResult> UpdatePassword([FromServices] IUserUp userService, AlterPasswordUpDto user)
        {
            var alterPassword = await userService.AlterPassword(user);
            return this.StatusCode(StatusCodes.Status200OK, alterPassword);
        }

        [HttpDelete("{id}", Name = "Delete user from the ID")]
        public async Task<IActionResult> Delete([FromServices] IDeleteUser userService, int id)
        {
            var user = await _getUser1.SearchUserIdAsync(id);
            await userService.DeleteAsync(user);

            return this.StatusCode(StatusCodes.Status202Accepted, user);
        }

    }
}


