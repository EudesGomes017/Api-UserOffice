using Data.Context;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Data.RepositoryData;

public class AddressRegisterRepositoryData : GeralRepositoryData, IAddressRegisterDomain
{
    private readonly ApiUserOfficeContext _apiUserOfficeContext;

    public AddressRegisterRepositoryData(ApiUserOfficeContext apiUserOfficeContext) : base(apiUserOfficeContext)
    {
    }

    public async Task<AddressRegister> CepAsync(string? cep)
    {
        IQueryable<AddressRegister> query = _apiUserOfficeContext.AddressRegister;

        query = query.AsNoTracking()
                     .Include(c => c.User)
                     .Where(x => x.Cep == cep)
                     .OrderBy(x => x.Id);

        return await query.FirstOrDefaultAsync();
    }
}

