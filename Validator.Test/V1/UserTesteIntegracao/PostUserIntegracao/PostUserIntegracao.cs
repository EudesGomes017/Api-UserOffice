using Api_UserOffice;
using Domain.Enums;
using FluentAssertions;
using Validator.Test.UtilsTeste;
using Xunit;

namespace Validator.Test.V1.UserTesteIntegracao.PostUserIntegracao;

public class PostUserIntegracao : ControllerBase
{

    private const string METODO = "/user";
    public PostUserIntegracao(ApplicationFactory<Program> factory) : base(factory)
    {

    }

    [Fact]
    public async Task Validate_Sucesso()
   {
        var Requisition =  RequestUserBuilder.Build();

        Requisition.Person = (StatusUser)1;
        Requisition.Role = "Administrador";
        Requisition.FancyName = "D.L.E.A";
        Requisition.IsActive = true;
        Requisition.logradouro = "patativa";
        Requisition.bairro = "paratibe";
        Requisition.complemento = "casa";
        Requisition.cep = "58066145";
        Requisition.numero_da_casa = "100";
        Requisition.uf = "pb";
        Requisition.localidade = "João Pessoa";
        Requisition.CreatedAt = DateTime.Now;

        var answer = await PostRequest(METODO, Requisition);
        answer.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

    }
}