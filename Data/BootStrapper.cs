using Domain.Services.serviceUser.Criptorgrafia;
using Domain.Token;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Data;

public static class BootStrapper
{

    
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {

        AdicionalChaveSenha(services, configuration);
        AdicionalTokenJwt(services, configuration);
        //AddFluentMigrator(services, configuration);

    }

    private static void AdicionalChaveSenha(IServiceCollection services, IConfiguration configuration)
    {


       // bool.TryParse(configuration.GetSection("ConfigurationMemory:BancoDeDadosInMemory").Value, out bool BancoDeDadosInMemory);

       // if (!BancoDeDadosInMemory)
        
            var section = configuration.GetRequiredSection("Configuration:ChaveAdicionalSenha");

            services.AddScoped(options => new EncryptPassword(section.Value));
             
    }

    private static void AdicionalTokenJwt(IServiceCollection services, IConfiguration configuration)
    {

       // bool.TryParse(configuration.GetSection("ConfigurationMemory:BancoDeDadosInMemory").Value, out bool BancoDeDadosInMemory);

       // if (!BancoDeDadosInMemory)
        
            var sectionTempoDeVidaToken = configuration.GetRequiredSection("Configuration:TempoDeVidaToken");
            var sectionKey = configuration.GetRequiredSection("Configuration:ChaveToken");

            services.AddScoped(options => new TokenController(int.Parse(sectionTempoDeVidaToken.Value), sectionKey.Value));
        
       
    }

    //private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    //{

    //    bool.TryParse(configuration.GetSection("ConfigurationMemory:BancoDeDadosInMemory").Value, out bool BancoDeDadosInMemory);

    //    if (!BancoDeDadosInMemory)
    //    {
    //        services.AddFluentMigratorCore()
    //         .ConfigureRunner(builder => builder
    //             .AddSqlServer()
    //             .WithGlobalConnectionString(configuration.GetConnectionString("ApiUserOffice")) // Substitua "NomeDaSuaConnectionString" pelo nome da sua conexão na configuração.
    //             .ScanIn(Assembly.Load("Data")).For.All());
    //    }

    //}
}

