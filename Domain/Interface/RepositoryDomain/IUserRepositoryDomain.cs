using Domain.Models;

namespace Domain.Interface.RepositoryDomain;

public interface IUserRepositoryDomain
{
    Task<User> UserByIdAsync(int? id);
    Task<User> UserByEmailAsync(string? email);
    Task<User[]> AllUsersAsync();
}

