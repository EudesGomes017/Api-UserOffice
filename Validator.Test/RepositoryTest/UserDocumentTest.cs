using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Services.serviceUser.services.SharedUser;
using Moq;

namespace Validator.Test.RepositoryTest;

public class UserDocumentTest
{
    private static UserDocumentTest _intance;
    private readonly Mock<IVerifyDocument> _IVerificaDocument;

    private UserDocumentTest()
    {
        if (_IVerificaDocument == null)
        {
            _IVerificaDocument = new Mock<IVerifyDocument>();
        }
    }

    public static UserDocumentTest UserInstantiates()
    {
        _intance = new UserDocumentTest();

        return _intance;
    }
    public UserDocumentTest ExisteUserDocument(string document)
    {
        if (!string.IsNullOrEmpty(document))
            _IVerificaDocument.Setup(i => i.SearchrDocument(document)).ReturnsAsync(true);

        return this;
    }
    public UserDocumentTest ExistsDocumentEmail(string document)
    {
        if (!string.IsNullOrEmpty(document))
            _IVerificaDocument.Setup(i => i.SearchrDocument(document)).ReturnsAsync(true);

        return this;
    }
    public IVerifyDocument build()
    {
        return _IVerificaDocument.Object;
    }
}
