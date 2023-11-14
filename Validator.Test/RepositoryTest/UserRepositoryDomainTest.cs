using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Moq;
using Validator.Test.AlterSenhaTest.IuserMockTest;

namespace Validator.Test.RepositoryTest;

public class UserRepositoryDomainTest
{
    private static UserRepositoryDomainTest _intance;
    private readonly Mock<IGetUserRepositoryDomain> _repositoryDomain;
    
    private UserRepositoryDomainTest()
    {
        if ( _repositoryDomain == null )
        {
            _repositoryDomain = new Mock<IGetUserRepositoryDomain>();
        }
    }

    public static UserRepositoryDomainTest UserInstantiates()
    {
        _intance = new UserRepositoryDomainTest();

        return _intance;
    }

    public UserRepositoryDomainTest RecoverId(User user)
    {
        _repositoryDomain.Setup(c => c.UserByIdAsync(user.Id)).ReturnsAsync(user);
        return this;
    }

    public IGetUserRepositoryDomain build()
    {
        return _repositoryDomain.Object;
    }
}

