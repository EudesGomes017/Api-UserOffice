using Data.Context;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.RepositoryData;

public class UserRepositoryData : GeralRepositoryData, IUserRepositoryDomain
{
    private readonly ApiUserOfficeContext _apiUserOfficeContext;
    public UserRepositoryData(ApiUserOfficeContext apiUserOfficeContext) : base(apiUserOfficeContext)
    {
        _apiUserOfficeContext = apiUserOfficeContext;
    }

    public async Task<User[]> AllUsersAsync()
    {
        IQueryable<User> query = _apiUserOfficeContext.User;

        query = query.AsNoTracking()
                       .Include(c => c.Departments)
                       .Include(c => c.AddressRegister)
                       .OrderBy(x => x.Id);

        return await query.ToArrayAsync();
    }

    public async Task<User> UserByEmailAsync(string? email)
    {
        IQueryable<User> query = _apiUserOfficeContext.User;

        query = query.AsNoTracking()
                     .Include(c => c.Departments)
                     .Include(c => c.AddressRegister)
                     .Where(x => x.Email == email)
                     .OrderBy(x => x.Id);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<User> UserByIdAsync(int? id)
    {
        IQueryable<User> query = _apiUserOfficeContext.User;

        query = query.AsNoTracking()
                      .Include(c => c.Departments)
                      .Include(c => c.AddressRegister)
                     .Where(x => x.Id == id)
                     .OrderBy(x => x.Id);

        return await query.FirstOrDefaultAsync();
    }
}

