using Domain.Services.serviceUser.AuthUser;

namespace Domain.Dto;

public class LoginUserDto
{
    public string? Password { get; set; }
    public string? Email { get; set; }
}
