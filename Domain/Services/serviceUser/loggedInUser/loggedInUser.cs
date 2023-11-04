using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Token;
using Microsoft.AspNetCore.Http;

namespace Domain.Services.serviceUser.loggedInUser
{
    public class loggedInUser : IloggedInUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenController _tokenController;
        private readonly Interface.RepositoryDomain.IGetUserRepositoryDomain _userRepositoryDomain;
        public loggedInUser(IHttpContextAccessor httpContextAccessor, TokenController tokenController, Interface.RepositoryDomain.IGetUserRepositoryDomain userRepositoryDomain)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenController = tokenController;
            _userRepositoryDomain = userRepositoryDomain;
        }
        public async Task<User> RecoverLogin()
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var token = authorization["Bearer".Length..].Trim(); //APARTI DA POSIÇÃO 6

            var emailUser = _tokenController.RecoverEmail(token);

            var user =  await _userRepositoryDomain.UserByEmailAsync(emailUser);
            return user;
        }
    }
}
