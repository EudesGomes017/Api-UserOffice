using Domain.Dto;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_UserOffice.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       // [Authorize(Roles = "Administrador")]
        [HttpPost(Name = "Adcionar Usuário")]
        public async Task<IActionResult> Post([FromServices] IPostUser userService, UserDto model)
        {
            var user = await userService.AddUserAsync(model);
            return this.StatusCode(StatusCodes.Status201Created, user);
        }

    }
}