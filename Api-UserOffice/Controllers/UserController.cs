using Domain.Dto;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Microsoft.AspNetCore.Mvc;

namespace Api_UserOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpPost(Name = "Adcionar Usuário")]
        public async Task<IActionResult> Post([FromServices] IPostUser userService, UserDto model)
        {
            var user = await userService.AddUserAsync(model);
            return this.StatusCode(StatusCodes.Status201Created, user);
        }
    }
}