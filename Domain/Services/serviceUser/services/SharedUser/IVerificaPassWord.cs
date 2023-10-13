namespace Domain.Services.serviceUser.services.SharedUser
{
    public interface IVerificaPassWord
    {
        Task<bool> SearchrDocument(string document);
    }
}