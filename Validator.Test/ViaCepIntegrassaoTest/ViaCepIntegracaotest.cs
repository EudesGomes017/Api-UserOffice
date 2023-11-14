using Domain.Integracao.Refit;
using Refit;
using Xunit;

namespace Validator.Test.ViaCepIntegrassaoTest;

public class ViaCepTest
{
    private readonly IViacepRefit _viaCep;

    public ViaCepTest()
    {
        // Aqui você pode configurar o Refit para usar um HttpClient real
        _viaCep = RestService.For<IViacepRefit>("https://viacep.com.br");
    }

    [Fact]
    public async Task Deve_Obter_Dados_Do_Endereco_Pelo_Cep_RetoraEnderessoViaCep()
    {
        string cep = "58066-145";

        var resultado = await _viaCep.ObterDadosViaCep(cep);

        Assert.NotNull(resultado);
        Assert.True(resultado.IsSuccessStatusCode);
        Assert.NotNull(resultado.Content);
        Assert.Equal(cep, resultado.Content.cep);

    }

    //[Fact]
    //public async Task ObterDadosViaCep_RetornaEndereçoComViaCep()
    //{
    //    var requisicao = "58066145";
    //    var createPost = ObterDadosViaCep(requisicao);

    //    Func<Task> acao = async () => { await createPost.ObterDadosViaCep(requisicao); };

    //    await acao.Should().NotThrowAsync(); // essa ação não deve gerar uma exeção
    //}

    //private ViaCepIntegracao ObterDadosViaCep(string cep)
    //{
    //    var repositoryReadOnly = ViaCepReadTest.UserInstantiates().ExisteUserCep(cep).build();

    //    return new ViaCepIntegracao(repositoryReadOnly);
    //}
}