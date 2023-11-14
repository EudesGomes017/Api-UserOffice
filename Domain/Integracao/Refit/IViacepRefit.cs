using Domain.Dto;
using Refit;

namespace Domain.Integracao.Refit;

public interface IViacepRefit
{
    [Get("/ws/{cep}/json")] //deixando dinânmico
    Task<ApiResponse<UserAndressDto>>  ObterDadosViaCep(string cep);
}
