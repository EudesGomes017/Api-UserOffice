namespace Domain.Services.serviceUser.InterfaceUsersServices
{
    public interface IVerificarDocumento
    {
        Task<bool> SearchrDocument(string document);
    }
}