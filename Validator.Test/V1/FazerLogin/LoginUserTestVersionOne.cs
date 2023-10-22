using Api_UserOffice;
using Domain.Dto;
using Domain.Models;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace Validator.Test.V1.Login;

public class LoginUserTestVersionOne : ControllerBase
{
    private const string METODO = "/loginuser";

    private User _user;
    private string password;
    public LoginUserTestVersionOne(ApplicationFactory<Program> factory) : base(factory)
    {
        _user = factory.RecuperarUser();
        password = factory.RecuperarPassword();
    }

    [Fact]
    public async Task Valida_Sucesso()
    {
        var requeisicao =  new LoginUserDto
        {
            Email = _user.Email,
            Password = password

        };

        //requeisicao.Role = "administrador";
        //requeisicao.Person = (StatusUser)1;
        //requeisicao.Role = "Administrador";
        //requeisicao.FancyName = "D.L.E.A";
        //requeisicao.IsActive = true;

        var resposta = await PostRequest(METODO, requeisicao); 

        resposta.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

      await using var respotBody = await resposta.Content.ReadAsStreamAsync();
   
        var respotaData = await JsonDocument.ParseAsync(respotBody);

       // respotaData.RootElement.GetProperty("email").GetString().Should().NotBeNullOrWhiteSpace().And.Be(user.Email);
        respotaData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();

    }
}
