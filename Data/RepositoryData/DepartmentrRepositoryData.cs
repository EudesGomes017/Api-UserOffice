using Data.Context;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.RepositoryData;

public class DepartmentrRepositoryData : GeralRepositoryData, IDepartmentDomain
{
    private readonly ApiUserOfficeContext _apiUserOfficeContext;
    public DepartmentrRepositoryData(ApiUserOfficeContext apiUserOfficeContext) : base(apiUserOfficeContext)
    {
    }

    public async Task<Department[]> AllDepartmentAsync()
    {
        IQueryable<Department> query = _apiUserOfficeContext.Department;

        query = query.AsNoTracking()
                       .Include(c => c.user)
                       .OrderBy(x => x.Id);

        return await query.ToArrayAsync();
    }

    public async Task<Department> DepartmentByIdAsync(int? id)
    {
        IQueryable<Department> query = _apiUserOfficeContext.Department;

        query = query.AsNoTracking()
                     .Include(c => c.user)
                     .Where(x => x.Id == id)
                     .OrderBy(x => x.Id);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Department> ResponsiblelAsync(string? responsible)
    {
        IQueryable<Department> query = _apiUserOfficeContext.Department;

        query = query.AsNoTracking()
                     .Include(c => c.user)
                     .Where(x => x.Responsible == responsible)
                     .OrderBy(x => x.Id);

        return await query.FirstOrDefaultAsync();
    }
}

