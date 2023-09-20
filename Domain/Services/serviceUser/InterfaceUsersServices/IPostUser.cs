using Domain.Dto;
using Domain.Shared;

namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IPostUser
    {
        Task<ReplyJsonRegisteredUser> AddUserAsync(UserDto modelUser);
    }
}