using Domain.Services.serviceUser.services.SharedUser;
using Moq;

namespace Validator.Test.RepositoryTest;

public class UserPassword
{
    private static UserPassword _intance;
    private readonly Mock<IVerificaPassWord> _IVerificaPassWord;

    private UserPassword()
    {
        if (_IVerificaPassWord == null)
        {
            _IVerificaPassWord = new Mock<IVerificaPassWord>();
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
            _IVerificaPassWord.Setup(i => i.SearchrDocument(document)).ReturnsAsync(true);

        return this;
    }
    public UserPassword ExisteDocumentEmail(string document)
    {
        if (!string.IsNullOrEmpty(document))
            _IVerificaPassWord.Setup(i => i.SearchrDocument(document)).ReturnsAsync(true);

        return this;
    }
    public IVerificaPassWord build()
    {
        return _IVerificaPassWord.Object;
    }
}
