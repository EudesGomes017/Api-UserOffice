using Domain.Dto;

namespace Domain.Services.serviceUser.AuthUser
{
    public interface ILoginUser
    {
        Task<object> UserByEmailAsync(LoginUserDto userLogin);
    }
}