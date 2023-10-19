using Domain.Interface.RepositoryDomain;

namespace Domain.Services.serviceUser.services.SharedUser;

public class VerificaPassWord : IVerificaPassWord
{
    private readonly IUserRepositoryDomain _userRepositoryDomain;

    public VerificaPassWord(IUserRepositoryDomain userRepositoryDomain)
    {
        _userRepositoryDomain = userRepositoryDomain;
    }
    public async Task<bool> SearchrDocument(string password)
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
