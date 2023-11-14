using Domain.Dto;

namespace Domain.Integracao.Interface
{
    public interface IViaCepIntegracao
    {
        Task<UserAndressDto> ObterDadosViaCep(string cep);
    }
}