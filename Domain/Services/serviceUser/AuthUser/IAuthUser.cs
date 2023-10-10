using Domain.Dto;

namespace Domain.Services.serviceUser.AuthUser
{
    public interface IAuthUser
    {
        Task<object> UserByEmailAsync(AuthDto userLogin);
    }
}