using Domain.Dto;

namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IUserUp
    {
        Task<UserDto> UpUserAsync(UserDto modelUser);
        Task<string> IsActiveUserAsync(string email);
    }
}