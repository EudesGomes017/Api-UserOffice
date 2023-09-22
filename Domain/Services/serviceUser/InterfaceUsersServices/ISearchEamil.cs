using Domain.Dto;

namespace Domain.Services.serviceUser.InterfaceUsersServices;

public interface ISearchEamil
{
    Task<UserDto> SearchEamil(string email);
}

