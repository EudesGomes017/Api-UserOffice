using Domain.Dto;

namespace Domain.Services.serviceUser;

public interface ISearchEamil
{
    Task<UserDto> SearchEamil(string email);
}

