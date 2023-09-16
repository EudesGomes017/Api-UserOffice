using Domain.Models;

namespace Domain.Interface.RepositoryDomain
{
    public interface IAddressRegisterDomain
    {
        Task<AddressRegister> CepAsync(string? cep);

    }
}
