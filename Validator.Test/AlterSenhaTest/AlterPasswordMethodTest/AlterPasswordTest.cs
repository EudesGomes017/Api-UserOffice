using Domain.Dto;
using Domain.Models;
using Domain.Services.serviceUser.services;
using FluentAssertions;
using Validator.Test.AlterSenhaTest.loggedInUser;
using Validator.Test.RepositoryTest;
using Validator.Test.UtilsTeste.EntidadesBuilderTest;
using Xunit;

namespace Validator.Test.AlterSenhaTest.AlterPasswordMethodTest;

public class AlterPasswordTest
{
    [Fact]
    public async Task TestAlterPassword()
    {
        (var user, var password) = UserBuilderTest.BuildUserAndPassword();

        var newPassword = CreateAlterPassword(user);

        var passwordDto = AlterPassword(password);

        Func<Task> action = async () =>
        {
            await newPassword.AlterPassword(passwordDto.ToString());
        };

        await action.Should().NotThrowAsync(); // essa ação não deve gerar uma exeção
    }

    private NewPassword CreateAlterPassword(User user)
    {

        var encryptador = EncryptPasswordTestBuilder.Instantiates();

        var userRepository = UserRepositoryDomainTest.UserInstantiates().RecoverId(user).build();

        var recoverLogin = LoggedInUser.UserInstantiates().RecoverLogin(user).build();


        return new NewPassword(userRepository, recoverLogin, encryptador);
    }

    private AlterPasswordUpDto AlterPassword(string password)
    {
        var teste = new AlterPasswordUpDto
        {
            Passwordnew = password                                  //Convert.ChangeType("", typeof(PasswordType)) as PasswordType
        };

        return teste;
    }
}
