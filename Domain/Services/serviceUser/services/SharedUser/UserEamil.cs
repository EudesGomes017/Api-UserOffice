using AutoMapper;
using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Token;

namespace Domain.Services.serviceUser.services.SharedUser;

public class UserEamil : ISearchEamil
{
    private readonly Interface.RepositoryDomain.IGetUserRepositoryDomain _userRepositoryDomain;
    private readonly IMapper _mapper;
    
    public UserEamil(Interface.RepositoryDomain.IGetUserRepositoryDomain userRepositoryDomain, IMapper mapper)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _mapper = mapper;

    }

    public async Task<bool> SearchrEmail(string email)
    {
        try
        {
            var result = await _userRepositoryDomain.UserByEmailAsync(email);

            return result != null;

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public async Task<UserDto> BuscaEamil(string email)
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

    public Task<bool> Test(long id)
    {
        throw new NotImplementedException();
    }


}


