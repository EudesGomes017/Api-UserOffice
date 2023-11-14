using Data.Context;
using Domain.Models;
using Validator.Test.UtilsTeste.EntidadesBuilderTest;

namespace Validator.Test.UtilsTeste;

public class ContexSeedInMemory
{
    public static (User user, string password) Seed(ApiUserOfficeContext context)
    {

        (var user, string password) = UserBuilderTest.BuildUserAndPassword();
        user.Id = 2;
        user.Email = "johndoe@gmail.com";
        user.FancyName = "teste";
        user.Role = "administrator";
        user.IsActive = true;
        user.bairro = "";
        user.uf = "";
        user.localidade = "";
        user.cep = "";
        user.complemento = "";
        user.numero_da_casa = "";
        user.logradouro = "";
        user.UpdateAt = DateTime.Now;
        context.User.Add(user);

        context.SaveChanges();

        return ( user, password);
    }
}
