namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IVerifyPassWord
    {
        Task<bool> SearchrPasssword(string document);
    }
}