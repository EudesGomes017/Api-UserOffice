using Domain.Interface.RepositoryDomain;
using Domain.Token;
using Exceptions;
using Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Api_UserOffice.filter.api;

public class UserAuthentication : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IGetUserRepositoryDomain _userRepositoryDomain;
    public UserAuthentication(TokenController tokenController, IGetUserRepositoryDomain userRepositoryDomain)
    {
        _tokenController = tokenController;
        _userRepositoryDomain = userRepositoryDomain;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        try
        {

            var token = TokenRequisition(context);

            var emailUser = _tokenController.RecoverEmail(token);

            var user = await _userRepositoryDomain.UserByEmailAsync(emailUser);

            if (user is null)
            {
                throw new System.Exception();
            }

        }
        catch (SecurityTokenEncryptionFailedException) // pega o token Expirado
        {
            TokenExpirado(context);
        }
        catch
        {
            UseCantAccess(context);
        }
       
    }
    private string TokenRequisition(AuthorizationFilterContext context)
    {
        var authoriz = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrWhiteSpace(authoriz))
        {
            throw new System.Exception();
        }

        var authorization =  authoriz["Bearer".Length..].Trim(); //APARTI DA POSIÇÃO 6

        return authorization;
    }

    private void TokenExpirado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseErroJson(ResourceMenssagensErro.TOKEN_EXPIRADO));
    }

    private void UseCantAccess(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseErroJson(ResourceMenssagensErro.USER_WITHOUT_PERMISE));
    }
}
