namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IVerifyDocument
    {
        Task<bool> SearchrDocument(string document);
    }
}