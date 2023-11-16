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
        _user = factory.RecoverUser();
        password = factory.RecoverPassword();
    }

    [Fact]
    public async Task Validates_Sucesso()
    {
        var requisition = new LoginUserDto
        {
            Email = _user.Email,
            Password = password
        };

        var answer = await PostRequest(METODO, requisition); 

        answer.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

      await using var answerBody = await answer.Content.ReadAsStreamAsync();
   
        var answerData = await JsonDocument.ParseAsync(answerBody);

       // answerData.RootElement.GetProperty("email").GetString().Should().NotBeNullOrWhiteSpace().And.Be(user.Email);
        answerData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();

    }
}
