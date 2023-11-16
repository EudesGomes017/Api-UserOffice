using AutoMapper;
using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Domain.Services.serviceUser.Criptorgrafia;
using Domain.Services.serviceUser.InterfaceUsersServices;

namespace Domain.Services.serviceUser.services;

public class UserUp : IUserUp
{

    private readonly IGetUserRepositoryDomain _userRepositoryDomain;
    private readonly IGetUserRepositoryDomainDto _getUser;
    private readonly IMapper _mapper;
    private readonly IloggedInUser _iloggedInUser;
    private readonly EncryptPassword _encryptPassword;


    public UserUp(IGetUserRepositoryDomain userRepositoryDomain, IGetUserRepositoryDomainDto getUser, IMapper mapper, IloggedInUser iloggedInUser, EncryptPassword encryptPassword)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _mapper = mapper;
        _getUser = getUser;
        _encryptPassword = encryptPassword;

    }

    public async Task<UserDto> UpUserAsync(UserDto modelUser)
    {
        try
        {
            var user = await _getUser.SearchUserIdAsync(modelUser.Id);

            if (user != null)
            {
                var Password = _encryptPassword.encrypt(modelUser.Password);
                var result = _mapper.Map<User>(modelUser);
                result.Password = Password;
                result.UpdateAt = DateTime.Now;
                result.CreatedAt = user.CreatedAt;
                _userRepositoryDomain.Atualizar(result);
                await _userRepositoryDomain.SalvarMudancasAsync();
                result.Password = "";
                return modelUser;
            }
            throw new Exception("Erro ao atualizar");
        }
        catch (Exception)
        {

            throw;
        }

    }
    public async Task<string> IsActiveUserAsync(string email)
    {
        try
        {
            var user = await _userRepositoryDomain.UserByEmailAsync(email);

            if (user != null)
            {

                user.UpdateAt = DateTime.Now;
                user.IsActive = !user.IsActive;
                _userRepositoryDomain.Atualizar(user);
                await _userRepositoryDomain.SalvarMudancasAsync();
                var statusCurrent = (bool)user.IsActive ? "Ativo" : "Inativo";
                return $"Status do Usuário {statusCurrent}";
            }
            throw new Exception("Erro ao atualizar");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
