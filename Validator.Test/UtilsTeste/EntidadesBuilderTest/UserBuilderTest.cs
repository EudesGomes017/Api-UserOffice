using Bogus;
using Bogus.Extensions.Brazil;
using Domain.Dto;
using Domain.Models;
using Validator.Test.RepositoryTest;

namespace Validator.Test.UtilsTeste.EntidadesBuilderTest;

public class UserBuilderTest    
{
    public static (User usuario, string email) Build()
    {
        string password = string.Empty;

        var resultadoGerador =  new Faker<User>()
            .RuleFor(c => c.Id, c => 1)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Password, f =>
            {
                password = f.Internet.Password();

               return EncryptPasswordTestBuilder.Instantiates().encrypt(password);
             
            })
            .RuleFor(c => c.Documento, f => f.Person.Cpf());

        return (resultadoGerador, password);
    }
}
