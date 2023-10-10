using Domain.Dto;
using Domain.Services.serviceUser.AuthUser;
using Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api_UserOffice.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost(Name = "Logar no sistema")]
    public async Task<IActionResult> Authentication([FromServices] IAuthUser userRepositoryDomain,   AuthDto user)
    {
        var userFind = await userRepositoryDomain.UserByEmailAsync(user);
            

        if (userFind == null)
        {
            return this.StatusCode(StatusCodes.Status401Unauthorized, new { messages = ResourceMenssagensErro.USER_FAIL_LOGIN });

        }

            return this.StatusCode(StatusCodes.Status200OK, userFind);
        
    }
}

