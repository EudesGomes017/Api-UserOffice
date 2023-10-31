using Api_UserOffice;
using Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
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
}
