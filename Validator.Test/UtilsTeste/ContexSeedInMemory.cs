using Data.Context;
using Domain.Enums;
using Domain.Models;
using Validator.Test.UtilsTeste.EntidadesBuilderTest;

namespace Validator.Test.UtilsTeste;

public class ContexSeedInMemory
{
    public static (User user, string password) Seed(ApiUserOfficeContext context)
    {

        (var user, string password) = UserBuilderTest.BuildUserAndPassword();
        user.Id = 2;
        user.Person = (StatusUser)3;
        user.Email = "johndoe@gmail.com";
        user.FancyName = "teste";
        user.Role = "administrator";
        user.IsActive = true;
        user.bairro = "valentina";
        user.uf = "jp";
        user.localidade = "joao pessoa";
        user.cep = "58066145";
        user.complemento = "casa";
        user.numero_da_casa = "1";
        user.logradouro = "sei la";
        user.UpdateAt = DateTime.Now;
        context.User.Add(user);

        context.SaveChanges();

        return ( user, password);
    }
}
