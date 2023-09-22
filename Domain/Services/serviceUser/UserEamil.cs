using AutoMapper;
using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Services.serviceUser.InterfaceUsersServices;

namespace Domain.Services.serviceUser;

public class UserEamil : ISearchEamil
{

    private readonly IUserRepositoryDomain _userRepositoryDomain;
    private readonly IMapper _mapper;

    public UserEamil(IUserRepositoryDomain userRepositoryDomain, IMapper mapper)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _mapper = mapper;
    }

    public async Task<UserDto> SearchEamil(string email)
    {
        UserDto user;

        try
        {
            var result = await _userRepositoryDomain.UserByEmailAsync(email);
            user = _mapper.Map<UserDto>(result);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return user;

    }
}


