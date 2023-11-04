using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Domain.Services.serviceUser.Criptorgrafia;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Services.serviceUser.loggedInUser;
using Exceptions;
using Exceptions.ExceptionBase;


namespace Domain.Services.serviceUser.services;

public class NewPassword : INewPassword
{
    private readonly IGetUserRepositoryDomain _userRepositoryDomain;
    private readonly IloggedInUser _iloggedInUser;
    private readonly EncryptPassword _encryptPassword;

    public NewPassword(IGetUserRepositoryDomain userRepositoryDomain, IloggedInUser iloggedInUser, EncryptPassword encryptPassword)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _iloggedInUser = iloggedInUser;
        _encryptPassword = encryptPassword;
    }

    public async Task<bool> AlterPassword(string alterPassword)
    {

        var logado = await User();

        var newPasword = new AlterPasswordUpDto
        {
            Passwordnew = alterPassword
        };  

        try
        {
            await validator(newPasword, logado);

            var newPassword = _encryptPassword.encrypt(alterPassword);

            logado.Password = newPassword;

            _userRepositoryDomain.Atualizar(logado);

            await _userRepositoryDomain.SalvarMudancasAsync();

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task validator(AlterPasswordUpDto alterPassword, User user)
    {
        var validator = new AlterPasswordValidator();
        var result = validator.Validate(alterPassword);

        if (string.IsNullOrWhiteSpace(alterPassword.Passwordnew)) 
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("password", ResourceMenssagensErro.PASSWORD_VAZIO));
        }

        var passwordUpdateencrypt = _encryptPassword.encrypt(alterPassword.Passwordnew);

        //verifica password é igual que foi salvo na banco de dados
        if (user.Password.Equals(passwordUpdateencrypt))
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("passwordUpdat", ResourceMenssagensErro.NOVO_PASSWORD_INVALID));
        }

        if (!result.IsValid)
        {
            var messageErro = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErroValidatorException(messageErro);
        }
    }

    public async Task<User> User()
    {
        var recoverLogin = await _iloggedInUser.RecoverLogin();

        var user = await _userRepositoryDomain.UserByIdAsync(recoverLogin.Id);

        return user;
    }
}
