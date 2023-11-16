using Api_UserOffice;
using Domain.Dto;
using Domain.Models;
using Exceptions;
using FluentAssertions;
using System.Text.Json;
using Validator.Test.V1;
using Xunit;

namespace Validator.Test.ViaCepIntegrassaoTest;

public class ViaCepAndPointTest : ControllerBase
{
    private const string METODO = "/api/cep";
    private const string METO = "/loginuser";
    private User _user;
    private string _password;
    private readonly ApplicationFactory<Program> _factory;

    public ViaCepAndPointTest(ApplicationFactory<Program> factory) : base(factory)
    {
        _factory = factory;
        _user = factory.RecoverUser();
        _password = factory.RecoverPassword();
        
    }

    [Fact]
    public async Task GetEndress_Cep_Retorn_Value_Ok()
    {
        var requisition = new LoginUserDto
        {
            Email = _user.Email,
            Password = _password
        };

        var token = await Login(requisition.Email, _password);

        var cep = "58066-145";

        var teste = await GetViaCep(METODO, new { Cep = cep }, token);
        teste.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAndress_Search_Value_Cep_Retorn_Erro_BadRequest()
    {
        var cep = "58066";

        var answer = await GetViaCep(METODO, new { Cep = cep });
        answer.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

        await using var answerBody = await answer.Content.ReadAsStreamAsync();

        var answerData = await JsonDocument.ParseAsync(answerBody);

        var erros = answerData.RootElement.GetProperty("messages").EnumerateArray();
        var mens = ResourceMenssagensErro.Culture = new System.Globalization.CultureInfo("en-US");
        erros.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceMenssagensErro.CEP_ERRADO));
    }

    [Fact]
    public async Task GetAndress_LookupZipCodeValue_Returns_Error_Empty_BadRequest_String_Empty()
    {
        var cep = string.Empty;

        var answer = await GetViaCep(METODO, new { Cep = cep });
        answer.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

}
