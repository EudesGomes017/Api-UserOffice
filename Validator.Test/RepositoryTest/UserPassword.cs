using Domain.Services.serviceUser.InterfaceUsersServices;
using Moq;

namespace Validator.Test.RepositoryTest;

public class UserPassword
{
    private static UserPassword _intance;
    private readonly Mock<IVerifyPassWord> _IVerifyPassWord;

    private UserPassword()
    {
        if (_IVerifyPassWord == null)
        {
            _IVerifyPassWord = new Mock<IVerifyPassWord>();
        }
    }

    public static UserPassword UserInstantiates()
    {
        _intance = new UserPassword();

        return _intance;
    }
    public UserPassword ExistePassWord(string document)
    {
        if (!string.IsNullOrEmpty(document))
            _IVerifyPassWord.Setup(i => i.SearchrPasssword(document)).ReturnsAsync(true);

        return this;
    }
    public UserPassword ExisteDocumentEmail(string document)
    {
        if (!string.IsNullOrEmpty(document))
            _IVerifyPassWord.Setup(i => i.SearchrPasssword(document)).ReturnsAsync(true);

        return this;
    }
    public IVerifyPassWord build()
    {
        return _IVerifyPassWord.Object;
    }
}
