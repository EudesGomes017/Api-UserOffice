using Domain.Dto;

namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IGetUser
    {
        Task<UserDto> SearchUserIdAsync(int? id);
        Task<UserDto[]> SearchAllUsersAsync();

    }
}