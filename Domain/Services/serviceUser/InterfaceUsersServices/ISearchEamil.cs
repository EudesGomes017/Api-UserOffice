namespace Domain.Services.serviceUser.InterfaceUsersServices;

public interface ISearchEamil
{
    Task<bool> ExisteUserEmail(string email);
    Task<bool> Test(long id);
}

