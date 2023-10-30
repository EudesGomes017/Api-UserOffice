using Data.Context;
using Domain.Models;
using Validator.Test.UtilsTeste.EntidadesBuilderTest;

namespace Validator.Test.UtilsTeste;

public class ContexSeedInMemory
{
    public static (User user, string password) Seed(ApiUserOfficeContext context)
    {

        (var user, string password) = UserBuilderTest.BuildUserAndPassword();
        user.Email = "johndoe@gmail.com";
        user.FancyName = "teste";
        user.Role = "administrator";
        user.IsActive = true;
        //user.AlterPassword = "";
        user.UpdateAt = DateTime.Now;
        context.User.Add(user);

        context.SaveChanges();

        return ( user, password);
    }
}
