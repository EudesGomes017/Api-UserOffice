using Bogus;
using Domain.Dto;

namespace Validator.Test.AlterSenhaTest
{
    public class ChangeRequestBuilder
    {
        public static AlterPasswordUpDto Builder(int tamanhoSenha = 10)
        {
            return new Faker<AlterPasswordUpDto>()
                .RuleFor(c => c.Passwordnew, f => f.Internet.Password(tamanhoSenha));
        }
    }
}
