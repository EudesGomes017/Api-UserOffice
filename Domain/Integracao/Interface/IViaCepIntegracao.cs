using Domain.Dto;

namespace Domain.Integracao.Interface
{
    public interface IViaCepIntegracao
    {
        Task<UserAndressDto> GetDataViaZipCode(string cep);
    }
}