using Domain.Models;

namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IloggedInUser
    {
        Task<User> RecoverLogin();

    }
}