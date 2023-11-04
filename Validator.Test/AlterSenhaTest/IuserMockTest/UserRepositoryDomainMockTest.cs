using Domain.Dto;
using Domain.Models;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Moq;

namespace Validator.Test.AlterSenhaTest.IuserMockTest;

public class UserRepositoryDomainMockTest
{
    private static UserRepositoryDomainMockTest _intance;
    private readonly Mock<IGetUserRepositoryDomainDto> _repositoryDomain;


    private UserRepositoryDomainMockTest()
    {
        if (_repositoryDomain == null)
        {
            _repositoryDomain = new Mock<IGetUserRepositoryDomainDto>();
        }
    }

    public static UserRepositoryDomainMockTest UserInstantiates()
    {
        _intance = new UserRepositoryDomainMockTest();

        return _intance;
    }


    public IGetUserRepositoryDomainDto build()
    {
        return _repositoryDomain.Object;
    }
}
