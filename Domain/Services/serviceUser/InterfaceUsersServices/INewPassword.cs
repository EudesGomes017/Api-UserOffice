using Domain.Models;

namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface INewPassword
    {
        Task<bool> AlterPassword(string alterPassword);
        Task<User> User();
    }
}