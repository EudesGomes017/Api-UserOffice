using Domain.Dto;
using Domain.Models;

namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IUserUp
    {
        Task<UserDto> UpUserAsync(UserDto modelUser);
        Task<string> IsActiveUserAsync(string email);
        Task<bool> AlterPassword(AlterPasswordUpDto user);
    }
}