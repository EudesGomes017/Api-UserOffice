using AutoMapper;
using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Domain.Services.serviceUser.InterfaceUsersServices;

namespace Domain.Services.serviceUser.services;

public class UserUp : IUserUp
{

    private readonly IUserRepositoryDomain _userRepositoryDomain;
    private readonly IGetUser _getUser;
    private readonly IMapper _mapper;

    public UserUp(IUserRepositoryDomain userRepositoryDomain, IMapper mapper, IGetUser getUser)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _mapper = mapper;
        _getUser = getUser;
    }
    public async Task<UserDto> UpUserAsync(UserDto modelUser)
    {
        try
        {
            var user = await _getUser.SearchUserIdAsync(modelUser.Id);

            if (user != null)
            {
                var result = _mapper.Map<User>(modelUser);
                result.UpdateAt = DateTime.Now;
               // result.CreatedAt = user.CreatedAt;
                _userRepositoryDomain.Atualizar(result);
                await _userRepositoryDomain.SalvarMudancasAsync();
                return modelUser;
            }
            throw new Exception("Erro ao atualizar");
        }
        catch (Exception)
        {

            throw;
        }

    }
}
