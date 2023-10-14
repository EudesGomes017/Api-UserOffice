using Domain.Dto;
using Domain.Services.serviceUser.AuthUser;
using Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api_UserOffice.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginUserController : ControllerBase
{
    [HttpPost(Name = "Logar no sistema")]
    public async Task<IActionResult> Authentication([FromServices] ILoginUser userRepositoryDomain, Domain.Dto.LoginUserDto user)
    {
        var userFind = await userRepositoryDomain.UserByEmailAsync(user);           

            return this.StatusCode(StatusCodes.Status200OK, userFind);
        
    }
}

