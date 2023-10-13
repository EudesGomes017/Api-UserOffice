using Domain.Dto;

namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IDeleteUser
    {
        Task<bool> DeleteAsync(UserDto id);
    }
}