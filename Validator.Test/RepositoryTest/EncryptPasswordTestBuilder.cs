using Domain.Interface.RepositoryDomain;
using Domain.Services.serviceUser.Criptorgrafia;
using Moq;

namespace Validator.Test.RepositoryTest;

public class EncryptPasswordTestBuilder
{
    public static EncryptPassword Instantiates()
    {
        return  new EncryptPassword("abc123");

    }

}

