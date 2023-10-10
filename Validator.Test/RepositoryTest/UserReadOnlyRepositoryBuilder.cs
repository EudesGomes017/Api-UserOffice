using Domain.Services.serviceUser.InterfaceUsersServices;
using Moq;

namespace Validator.Test.RepositoryTest;

public class UserReadOnlyRepositoryBuilder
{
    private static UserReadOnlyRepositoryBuilder _intance;
    private readonly Mock<ISearchEamil> _searEmail;

    private UserReadOnlyRepositoryBuilder()
    {
        if (_searEmail == null)
        {
            _searEmail = new Mock<ISearchEamil>();
        }
    }

    public static UserReadOnlyRepositoryBuilder UserInstantiates()
    {
        _intance = new UserReadOnlyRepositoryBuilder();

        return _intance;
    }
    public UserReadOnlyRepositoryBuilder ExisteUserEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
        _searEmail.Setup(i => i.SearchrEmail(email)).ReturnsAsync(true);

        return this;
    }
    public ISearchEamil build()
    {
        return _searEmail.Object;
    }
}
