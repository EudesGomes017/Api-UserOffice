using Domain.Dto;

namespace Domain.Interface.RepositoryDomain
{
    public interface IAddressRegisterDomain
    {
        Task<UserAndressDto> CepAsync(string? cep);
    }
}
