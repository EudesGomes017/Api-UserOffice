using Domain.Dto;

namespace Domain.Interface.ServicesRepository;

public interface IUserService
{
    Task<UserDto[]> SearchAllUsersAsync();
    Task<UserDto> SearchUserIdAsync(int id);
    Task<UserDto> SearchEamil(string email);
    Task<UserDto> UpUserAsync(UserDto modelUser);
    Task<bool> DeleteAsync(UserDto id);
}

