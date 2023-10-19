using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Services.serviceUser.services.SharedUser;
using Moq;

namespace Validator.Test.RepositoryTest;

public class UserDocumentTest
{
    private static UserDocumentTest _intance;
    private readonly Mock<IVerificarDocumento> _IVerificaDocument;

    private UserDocumentTest()
    {
        if (_IVerificaDocument == null)
        {
            _IVerificaDocument = new Mock<IVerificarDocumento>();
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
    public UserDocumentTest ExisteDocumentEmail(string document)
    {
        if (!string.IsNullOrEmpty(document))
            _IVerificaDocument.Setup(i => i.SearchrDocument(document)).ReturnsAsync(true);

        return this;
    }
    public IVerificarDocumento build()
    {
        return _IVerificaDocument.Object;
    }
}
