using Domain.Models;

namespace Domain.Interface.RepositoryDomain;

public interface IDepartmentDomain
{
    Task<Department> DepartmentByIdAsync(int? id);
    Task<Department> ResponsiblelAsync(string? responsible);
    Task<Department[]> AllDepartmentAsync();
}

