namespace Domain.Interface.RepositoryDomain;

public interface IGeralRepositoryDomain
{
    void Adicionar<T>(T entity) where T : class;
    void Atualizar<T>(T entity) where T : class;
    void Deletar<T>(T entity) where T : class;
    void DeletarVarias<T>(T[] entityArray) where T : class;
    Task<bool> SalvarMudancasAsync();
}

