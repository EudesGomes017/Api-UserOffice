using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Services.serviceUser.Criptorgrafia;
using Domain.Token;
using Exceptions.ExceptionBase;

namespace Domain.Services.serviceUser.AuthUser;

public class LoginUser : ILoginUser
{

    private readonly IGetUserRepositoryDomain _userRepositoryDomain;
    private readonly EncryptPassword _encryptPassword;
    private readonly TokenController _tokenController;

    public LoginUser(IGetUserRepositoryDomain userRepositoryDomain, EncryptPassword encryptPassword, TokenController tokenController)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _encryptPassword = encryptPassword;
        _tokenController = tokenController;
    }

    public async Task<Object> UserByEmailAsync(LoginUserDto userLogin)
    {
        var token = "";

        try
        {
            var result = await _userRepositoryDomain.UserByEmailAsync(userLogin.Email);


            if (result == null)
            {
                throw new LoginInvalideException();
            }

            var Password = _encryptPassword.encrypt(userLogin.Password);

            if (Password != result.Password)
            {
                throw new LoginInvalideException();
            }

            if (Password == result.Password)
            {
                token = _tokenController.GenerateToken(result);
            }

            return new { Id = result.Id, Name = result.Name, Email = result.Email, Token = token };

        }

        catch (LoginInvalideException ex)
        {
            throw ex;
        }


    }
}
