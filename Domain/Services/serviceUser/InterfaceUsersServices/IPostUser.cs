using Domain.Dto;
using Domain.Models;
using Domain.Shared;

namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IPostUser
    {
        Task<User> AddUserAsync(UserDto modelUser);
      
    }
}