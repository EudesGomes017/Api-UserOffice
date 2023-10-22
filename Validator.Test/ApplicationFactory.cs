using Data.Context;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;
using Validator.Test.UtilsTeste;

namespace Validator.Test;

public class ApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private User _user;
    private string password;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test") // api em execução modo de Teste
            .ConfigureServices(services =>
            {

                var descritor = services.SingleOrDefault(d => d.ServiceType == typeof(ApiUserOfficeContext)); // se existe em memoria remove e usa o novo 

                if (descritor != null)
                {
                    services.Remove(descritor);
                }

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<ApiUserOfficeContext>(options =>
                {
                    options.UseInMemoryDatabase("ConfigurationMemory");
                    options.UseInternalServiceProvider(provider);
                });

                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();

                var scopeService = scope.ServiceProvider;

                var dataBase = scopeService.GetRequiredService<ApiUserOfficeContext>();

                dataBase.Database.EnsureDeleted();

                (_user, password) = ContexSeedInMemory.Seed(dataBase);
            });
    }

    public User RecuperarUser()
    {
        return _user;
    }

    public string RecuperarPassword()
    {
        return password;
    }
}
