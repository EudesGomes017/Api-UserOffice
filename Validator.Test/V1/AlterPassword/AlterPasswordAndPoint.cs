using Api_UserOffice;
using Domain.Models;
using Exceptions;
using FluentAssertions;
using System.Text.Json;
using Validator.Test.AlterSenhaTest;
using Xunit;

namespace Validator.Test.V1.AlterPassword;

public class AlterPasswordAndPoint : ControllerBase
{
    private const string METODO = "/user/uppassword/2";

    private User _user;
    private string _password;
    private string _newPassword = "123456789";
    public AlterPasswordAndPoint(ApplicationFactory<Program> factory) : base(factory)
    {
        _user = factory.RecoverUser();
        _password = factory.RecoverPassword();      
    }

    [Fact]
    public async Task Validates_Sucesso()
    {

        var token = await Login(_user.Email, _password);

        var requisition = ChangeRequestBuilder.Builder();

        requisition.Passwordnew = _newPassword;

        var answer = await PutRequest(METODO, requisition, token);

        answer.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

    }

    [Fact]
    public async Task Validates_Erro_PasswordEmpety()
    {

        var token = await Login(_user.Email, _password);

        var requisition = ChangeRequestBuilder.Builder();
        requisition.Passwordnew = _password;
        requisition.Passwordnew = string.Empty;

        var answer = await PutRequest(METODO, requisition, token);

        answer.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

        await using var answerBody = await answer.Content.ReadAsStreamAsync();

        var answerData = await JsonDocument.ParseAsync(answerBody);

        var erros = answerData.RootElement.GetProperty("messages").EnumerateArray();
        var mens = ResourceMenssagensErro.Culture = new System.Globalization.CultureInfo("en-US");
        erros.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceMenssagensErro.PASSWORD_VAZIO));

    }
}
