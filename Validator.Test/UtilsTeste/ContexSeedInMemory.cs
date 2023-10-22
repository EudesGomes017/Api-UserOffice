using Data.Context;
using Domain.Models;
using Validator.Test.UtilsTeste.EntidadesBuilderTest;

namespace Validator.Test.UtilsTeste;

public class ContexSeedInMemory
{
    public static (User user, string password) Seed(ApiUserOfficeContext context)
    {

        (var user, string password) = UserBuilderTest.Build();
        user.Email = "johndoe@gmail.com";
        user.FancyName = "teste";
        user.Role = "administrador";
        user.IsActive = true;
        user.UpdateAt = DateTime.Now;
        context.User.Add(user);

        context.SaveChanges();

        return ( user, password);
    }
}
