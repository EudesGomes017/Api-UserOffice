using Domain.Dto;
using Domain.Integracao.Refit;
using Moq;
using Refit;
using System.Net;
using Validator.Test.RepositoryTest;

namespace Validator.Test.ViaCepIntegrassaoTest;

public class ViaCepReadTest
{
    private static ViaCepReadTest _intance;
    private readonly Mock<IViacepRefit> _viaCep;

    private ViaCepReadTest()
    {
        if (_viaCep == null)
        {
            _viaCep = new Mock<IViacepRefit>();
        }
    }

    public static ViaCepReadTest UserInstantiates()
    {
        _intance = new ViaCepReadTest();

        return _intance;
    }

    public ViaCepReadTest ExisteUserCep(string cep)
    {
        if (!string.IsNullOrEmpty(cep))
        {
            var userAddressDto = new UserAndressDto
            {
                // Preencha com dados fictícios correspondentes ao seu modelo UserAndressDto
                // Exemplo:
                cep = cep,
                //logradouro = "Rua Teste",
                //bairro = "Bairro Teste",
                //localidade = "Cidade Teste",
                //uf = "TS"
            };

            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var apiResponse = new ApiResponse<UserAndressDto>(httpResponse, userAddressDto, new RefitSettings());

            var ver = _viaCep.Setup(i => i.ObterDadosViaCep(cep)).ReturnsAsync(apiResponse);
        }

        return this;
    }




    public IViacepRefit build()
    {
        return _viaCep.Object;
    }
}

