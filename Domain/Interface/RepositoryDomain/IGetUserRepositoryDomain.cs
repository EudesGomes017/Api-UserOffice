using Domain.Models;
namespace Domain.Interface.RepositoryDomain;

public interface IGetUserRepositoryDomain : IGeralRepositoryDomain
{
    Task<User> UserByIdAsync(int? id);
    Task<User> UserByEmailAsync(string? email);
    Task<User> UserDocument(string? documento);
    Task<User> UserPassword(string? password);
    Task<User[]> AllUsersAsync();
}

