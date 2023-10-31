using Domain.Interface.RepositoryDomain;
using Domain.Services.serviceUser.InterfaceUsersServices;

namespace Domain.Services.serviceUser.services.SharedUser;

public class VerificaPassWord : IVerifyPassWord
{
    private readonly IUserRepositoryDomain _userRepositoryDomain;

    public VerificaPassWord(IUserRepositoryDomain userRepositoryDomain)
    {
        _userRepositoryDomain = userRepositoryDomain;
    }
    public async Task<bool> SearchrPasssword(string password)
    {
        try
        {

            var result = await _userRepositoryDomain.UserPassword(password);

            return result != null;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
