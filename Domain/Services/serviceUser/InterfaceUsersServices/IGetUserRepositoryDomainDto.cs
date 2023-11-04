using Domain.Dto;

namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IGetUserRepositoryDomainDto
    {
        Task<UserDto> SearchUserIdAsync(int? id);
        Task<UserDto[]> SearchAllUsersAsync();

    }
}