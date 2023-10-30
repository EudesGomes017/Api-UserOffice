using AutoMapper;
using Domain.Interface.RepositoryDomain;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Token;

namespace Domain.Services.serviceUser.services.SharedUser;

public class VerificarDocumento : IVerifyDocument
{
    private readonly IUserRepositoryDomain _userRepositoryDomain;

    public VerificarDocumento(IUserRepositoryDomain userRepositoryDomain, IMapper mapper, TokenController tokenController)
    {
        _userRepositoryDomain = userRepositoryDomain;

    }

    public async Task<bool> SearchrDocument(string document)
    {
        try
        {

            var result = await _userRepositoryDomain.UserDocument(document);

            return result != null;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
