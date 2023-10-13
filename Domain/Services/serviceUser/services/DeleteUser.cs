using AutoMapper;
using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Domain.Services.serviceUser.InterfaceUsersServices;

namespace Domain.Services.serviceUser.services;

public class DeleteUser : IDeleteUser
{

    private readonly IUserRepositoryDomain _userRepositoryDomain;
    private readonly IMapper _mapper;

    public DeleteUser(IUserRepositoryDomain userRepositoryDomain, IMapper mapper)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _mapper = mapper;
    }

    public async Task<bool> DeleteAsync(UserDto id)
    {
        try
        {
            var userCurrent = _mapper.Map<User>(id);
            _userRepositoryDomain.Deletar(userCurrent);

            await _userRepositoryDomain.SalvarMudancasAsync();

            if (userCurrent != null)
            {
                return true;
            }


            throw new Exception("Erro ao Deletar id");
        }
        catch (Exception)
        {

            throw new Exception("Erro de servidor");
        }
    }
}

