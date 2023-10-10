using Domain.Interface.RepositoryDomain;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Moq;

namespace Validator.Test.RepositoryTest;

public class UserRepositoryDomainBuilderTest
{
    private static UserRepositoryDomainBuilderTest _intance;
    private readonly Mock<IUserRepositoryDomain> _repositoryDomain;
   
    
    private UserRepositoryDomainBuilderTest()
    {
        if ( _repositoryDomain == null )
        {
            _repositoryDomain = new Mock<IUserRepositoryDomain>();
        }

       
    }

    public static UserRepositoryDomainBuilderTest UserInstantiates()
    {
        _intance = new UserRepositoryDomainBuilderTest();

        return _intance;
    }

    public IUserRepositoryDomain build()
    {
        return _repositoryDomain.Object;
    }
}

