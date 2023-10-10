using AutoMapper;
using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Token;

namespace Domain.Services.serviceUser;

public class UserEamil : ISearchEamil
{

    private readonly IUserRepositoryDomain _userRepositoryDomain;
    private readonly IMapper _mapper;
    private readonly TokenController _tokenController;

    public UserEamil(IUserRepositoryDomain userRepositoryDomain, IMapper mapper, TokenController tokenController)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _mapper = mapper;
        _tokenController = tokenController;
    }

    public async Task<bool> SearchrEmail(string email)
    {
        bool user;

        try
        {
            
            var result = await _userRepositoryDomain.UserByEmailAsync(email);
            user = _mapper.Map<bool>(result);

            return user;
        }
        catch (Exception ex)
        {
            throw ex;
        }
     

    }

    public Task<bool> Test(long id)
    {
        throw new NotImplementedException();
    }

    
}


