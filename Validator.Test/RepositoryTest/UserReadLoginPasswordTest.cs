﻿using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Moq;

namespace Validator.Test.RepositoryTest;

public class UserReadLoginPasswordTest
{
    private static UserReadLoginPasswordTest _intance;
    private readonly Mock<Domain.Interface.RepositoryDomain.IGetUserRepositoryDomain> _IUserRepositoryDomain;

    private UserReadLoginPasswordTest()
    {
        if (_IUserRepositoryDomain == null)
        {
            _IUserRepositoryDomain = new Mock<Domain.Interface.RepositoryDomain.IGetUserRepositoryDomain>();
        } 
    }

    public static UserReadLoginPasswordTest UserInstantiates()
    {
        _intance = new UserReadLoginPasswordTest();

        return _intance;
    }

    public UserReadLoginPasswordTest RecoverLoginPassword(User user)
    {
            _IUserRepositoryDomain.Setup(i => i.UserByEmailAsync(user.Email)).ReturnsAsync(user);
            _IUserRepositoryDomain.Setup(i => i.UserByEmailAsync(user.Password)).ReturnsAsync(user);

        return this;
    }

    public Domain.Interface.RepositoryDomain.IGetUserRepositoryDomain build()
    {
        return _IUserRepositoryDomain.Object;
    }
}
