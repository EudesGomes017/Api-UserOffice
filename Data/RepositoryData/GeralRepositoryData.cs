using Data.Context;
using Domain.Interface.RepositoryDomain;

namespace Data.RepositoryData;

public class GeralRepositoryData : IGeralRepositoryDomain
{
    private readonly ApiUserOfficeContext _apiUserOfficeContext;
    public GeralRepositoryData(ApiUserOfficeContext apiUserOfficeContext)
    {
        _apiUserOfficeContext = apiUserOfficeContext;
    }

    public async void Adicionar<T>(T entity) where T : class
    {
        await _apiUserOfficeContext.AddAsync(entity);
    }

    public void Atualizar<T>(T entity) where T : class
    {
        _apiUserOfficeContext.Update(entity);
    }

    public void Deletar<T>(T entity) where T : class
    {
        _apiUserOfficeContext.Remove(entity);
    }

    public void DeletarVarias<T>(T[] entityArray) where T : class
    {
        _apiUserOfficeContext.RemoveRange(entityArray);
    }

    public async Task<bool> SalvarMudancasAsync()
    {
        return (await _apiUserOfficeContext.SaveChangesAsync()) > 0;
    }
}

