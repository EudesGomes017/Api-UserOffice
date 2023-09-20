using Domain.Services.serviceUser.Criptorgrafia;
using Domain.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class BootStrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {

        AdicionalChaveSenha(services, configuration);
        AdicionalTokenJwt(services, configuration);

    }

    private static void AdicionalChaveSenha(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Configuration:ChaveAdicionalSenha");

        services.AddScoped(options => new EncryptPassword(section.Value));
    }

    private static void AdicionalTokenJwt(IServiceCollection services, IConfiguration configuration)
    {
        var sectionTempoDeVidaToken = configuration.GetRequiredSection("Configuration:TempoDeVidaToken");
        var sectionKey = configuration.GetRequiredSection("Configuration:ChaveToken");

        services.AddScoped(options => new TokenController(int.Parse(sectionTempoDeVidaToken.Value), sectionKey.Value));
    }
}

