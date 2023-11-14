using Bogus;
using Bogus.Extensions.Brazil;
using Domain.Dto;

namespace Validator.Test.UtilsTeste;

public class RequestUserBuilder
{
    public static UserDto Build(int size = 10)
    {
        return new Faker<UserDto>()
            //.RuleFor(c => c.Id, c => 1)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Password, f => f.Internet.Password(size))
            .RuleFor(c => c.Document, f => f.Person.Cpf());                   
    }
}

