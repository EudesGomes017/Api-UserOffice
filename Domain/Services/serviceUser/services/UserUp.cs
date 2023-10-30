using AutoMapper;
using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Domain.Services.serviceUser.Criptorgrafia;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Validators.ValidatorUser;
using Exceptions.ExceptionBase;
using Exceptions;
using Domain.Services.serviceUser.loggedInUser;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Web;

namespace Domain.Services.serviceUser.services;

public class UserUp : IUserUp
{

    private readonly IUserRepositoryDomain _userRepositoryDomain;
    private readonly IGetUser _getUser;
    private readonly IMapper _mapper;
    private readonly IloggedInUser _iloggedInUser;
    private readonly IVerifyPassWord _verifyPassWord;
    private readonly EncryptPassword _encryptPassword;


    public UserUp(IUserRepositoryDomain userRepositoryDomain, IMapper mapper, IGetUser getUser, IloggedInUser iloggedInUser, IVerifyPassWord verifyPassWord, EncryptPassword encryptPassword)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _mapper = mapper;
        _getUser = getUser;
        _iloggedInUser = iloggedInUser;
        _verifyPassWord = verifyPassWord;
        _encryptPassword = encryptPassword;

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
                result.CreatedAt = user.CreatedAt;
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

    public async Task<bool> AlterPassword(AlterPasswordUpDto user)
    {
        try
        {
         
            var recoverLogin = await _iloggedInUser.RecoverLogin();

            var userAnsewr = await _userRepositoryDomain.UserByIdAsync(recoverLogin.Id);

            validator(user);

            var newPassword = _encryptPassword.encrypt(user.SenhaNova);

            userAnsewr.Password = newPassword;

            var result = _mapper.Map<User>(userAnsewr);

            _userRepositoryDomain.Atualizar(result);

            await _userRepositoryDomain.SalvarMudancasAsync();    

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task validator(AlterPasswordUpDto user)
    {
        var recoverLogin = await _iloggedInUser.RecoverLogin();

        var pegaPassword = await _userRepositoryDomain.UserByIdAsync(recoverLogin.Id);

        var validator = new AlterPasswordValidator();
        var result = validator.Validate(user);

        var passwordUpdateencrypt = _encryptPassword.encrypt(user.SenhaNova);

        //verifica password é igual que foi salvo na banco de dados
        if (pegaPassword.Password.Equals(passwordUpdateencrypt))
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("passwordUpdat", ResourceMenssagensErro.NOVO_PASSWORD_INVALID));
        }

        if (!result.IsValid)
        {
            var messageErro = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErroValidatorException(messageErro);
        }
    }
}
