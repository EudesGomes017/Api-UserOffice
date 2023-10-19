using Domain.Dto;

namespace Domain.Services.serviceUser.InterfaceUsersServices;

public interface ISearchEamil
{
    Task<bool> SearchrEmail(string email);
    Task<UserDto> BuscaEamil(string email);
    Task<bool> Test(long id);
}

