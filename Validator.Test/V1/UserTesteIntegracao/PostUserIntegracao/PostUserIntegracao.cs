﻿using Api_UserOffice;
using Domain.Enums;
using FluentAssertions;
using System.Text.Json;
using Validator.Test.UtilsTeste;
using Xunit;

namespace Validator.Test.V1.UserTesteIntegracao.PostUserIntegracao;

public class PostUserIntegracao : ControllerBase
{

    private const string METODO = "user";
    public PostUserIntegracao(ApplicationFactory<Program> factory) : base(factory)
    {

    }

    [Fact]
    public async Task Valida_Sucesso()
    {
        var requeisicao =  RequestUserBuilder.Build();
         requeisicao.Person = (StatusUser)1;
        requeisicao.Role = "Administrador";
        requeisicao.FancyName = "D.L.E.A";

        var resposta = await PostRequest(METODO, requeisicao);
        resposta.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        //verificando o token
        await using var responseBody = await resposta.Content.ReadAsStreamAsync();

        var respostaData = await JsonDocument.ParseAsync(responseBody);

        respostaData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }
}