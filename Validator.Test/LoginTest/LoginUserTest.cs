using Bogus.DataSets;
using Domain.Dto;
using Domain.Models;
using Domain.Services.serviceUser.AuthUser;
using Exceptions;
using Exceptions.ExceptionBase;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Validator.Test.RepositoryTest;
using Validator.Test.UtilsTeste.EntidadesBuilderTest;
using Xunit;

namespace Validator.Test.LoginTest;

public class LoginUserTest
{
    [Fact]
    public async Task Validar_sucesso()
    {
        (var user, var password) = UserBuilderTest.Build();

        var userCase = CreateUser(user);
        user.Role = "administrador";
        var result = await userCase.UserByEmailAsync(new LoginUserDto
        {
            Email = user.Email,
            Password = password,

        });

        result.Should().NotBeNull();
        Convert.ToString(result.GetType().GetProperty("Email").GetValue(result)).Should().Be(user.Email);
        Convert.ToString(result.GetType().GetProperty("Password").GetValue(result)).Should().Be(user.Password);

    }

    [Fact]
    public async Task Validar_ErroPassword()
    {
        (var user, var password) = UserBuilderTest.Build();

        var userCase = CreateUser(user);
        user.Role = "administrador";
        Func<Task> action = async () =>
      {
          await userCase.UserByEmailAsync(new LoginUserDto
          {
              Email = user.Email,
              Password = "testetes"

          });
         
      };
       
        await action.Should().ThrowAsync<LoginInvalideException>()
            .Where(exception => exception.Message.Equals(ResourceMenssagensErro.USER_FAIL_LOGIN));
    }

    [Fact]
    public async Task Validar_ErroEmail()
    {
        (var user, var password) = UserBuilderTest.Build();

        var userCase = CreateUser(user);
        user.Role = "administrador";
        Func<Task> action = async () =>
        {
            await userCase.UserByEmailAsync(new LoginUserDto
            {
                Email = "teste",
                Password = password

            });

        };

        await action.Should().ThrowAsync<LoginInvalideException>()
            .Where(exception => exception.Message.Equals(ResourceMenssagensErro.USER_FAIL_LOGIN));
    }

    private LoginUser CreateUser(User user)
    {
        var encryptPassword = EncryptPasswordTestBuilder.Instantiates();
        var token = TokenTestBuilder.TokenInstantiates();
        var repositoryReadOnly = UserReadLoginPasswordTest.UserInstantiates().RecuperarLoginSenha(user).build();

        return new LoginUser(repositoryReadOnly, encryptPassword, token);
    }
}
