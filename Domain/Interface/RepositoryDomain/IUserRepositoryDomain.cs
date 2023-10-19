using Domain.Models;
namespace Domain.Interface.RepositoryDomain;

public interface IUserRepositoryDomain : IGeralRepositoryDomain
{
    Task<User> UserByIdAsync(int? id);
    Task<User> UserByEmailAsync(string? email);
    Task<User> UserDocument(string? documento);
    Task<User> UserPassword(string? password);
    Task<User[]> AllUsersAsync();
}

