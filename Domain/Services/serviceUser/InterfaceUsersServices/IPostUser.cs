using Domain.Dto;
using Domain.Models;

namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IPostUser
    {
        Task<User> AddUserAsync(UserDto modelUser);
     
    }
}