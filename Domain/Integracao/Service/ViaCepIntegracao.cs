using Domain.Dto;
using Domain.Integracao.Interface;
using Domain.Integracao.Refit;
using Exceptions;
using Exceptions.ExceptionBase;

namespace Domain.Integracao.Service;

public class ViaCepIntegracao : IViaCepIntegracao
{
    private readonly IViacepRefit _viacepRefit;
   
    public ViaCepIntegracao(IViacepRefit viacepRefit)
    {
        _viacepRefit = viacepRefit;

    }
    public async Task<UserAndressDto> ObterDadosViaCep(string cep)
    {
        var responseData = await _viacepRefit.ObterDadosViaCep(cep);

        if (responseData != null && responseData.IsSuccessStatusCode)
        {
            return responseData.Content;
        }

        throw new ErroValidatorException(new List<string> { ResourceMenssagensErro.CEP_ERRADO });
    }

}
