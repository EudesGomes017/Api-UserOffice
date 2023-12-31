﻿using Api_UserOffice;
using Domain.Dto;
using Domain.Models;
using Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Validator.Test.V1;

public class ControllerBase : IClassFixture<ApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    //inicializando o httpClient
	public ControllerBase(ApplicationFactory<Program> factory)
	{
        _httpClient = factory.CreateClient();
        ResourceMenssagensErro.Culture = CultureInfo.CurrentCulture;

    }

    protected async Task<HttpResponseMessage> PostRequest(string method, object body)
    {

        var jsonString = JsonConvert.SerializeObject(body);
        var result = await _httpClient.PostAsync(method, new StringContent(jsonString, Encoding.UTF8, "application/json"));
        return result;
    }

    protected async Task<HttpResponseMessage> PutRequest(string method, object body, string token = "")
    {
        AuthorizationRequest(token);

        var jsonString = JsonConvert.SerializeObject(body);
        var result = await _httpClient.PutAsync(method, new StringContent(jsonString, Encoding.UTF8, "application/json"));
        return result;
    }

    protected async Task<HttpResponseMessage> GetViaCep(string method, object parameters, string token = "")
    {

        AuthorizationRequest(token);

        var queryString = parameters != null
            ? "?" + string.Join("&", parameters.GetType().GetProperties()
                .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(parameters)?.ToString() ?? string.Empty)}"))
            : string.Empty;

        var result = await _httpClient.GetAsync($"{method}{queryString}");
        return result;
    }

    //protected async Task<HttpResponseMessage> GetViaCepNotFound(string method, object parameters)
    //{
    //    var queryString = parameters != null
    //        ? $"?{string.Join("&", parameters.GetType().GetProperties().Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(parameters)?.ToString() ?? string.Empty)}"))}"
    //        : string.Empty;

    //    return await _httpClient.GetAsync($"{method}{queryString}");
    //}

    protected async Task<string> Login(string email, string password)
    {
        var requisition = new LoginUserDto
        {
            Email = email,
            Password = password

        };

        var answer = await PostRequest("/loginuser", requisition);

        await using var answerBody = await answer.Content.ReadAsStreamAsync();

        var answerData = await JsonDocument.ParseAsync(answerBody);

        return answerData.RootElement.GetProperty("token").GetString();
    }

    private void AuthorizationRequest(string token)
    {
        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
    }

    
}
