using Domain.Models;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Moq;

namespace Validator.Test.AlterSenhaTest.loggedInUser;

public class LoggedInUser
{
    private static LoggedInUser _intance;
    private readonly Mock<IloggedInUser> _LoggedInUser;


    private LoggedInUser()
    {
        if (_LoggedInUser == null)
        {
            _LoggedInUser = new Mock<IloggedInUser>();
        }
    }

    public static LoggedInUser UserInstantiates()
    {
        _intance = new LoggedInUser();

        return _intance;
    }

    public LoggedInUser RecoverLogin(User user)
    {
        _LoggedInUser.Setup(c => c.RecoverLogin()).ReturnsAsync(user);
        return this;
    }

    public IloggedInUser build()
    {
        return _LoggedInUser.Object;
    }
}
