using Data;
using Data.Context;
using Data.RepositoryData;
using Domain.Interface.RepositoryDomain;
using Microsoft.EntityFrameworkCore;

namespace Api_UserOffice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddApplication(builder.Configuration);

            builder.Services.AddDbContext<ApiUserOfficeContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApiUserOffice"));
               // b => b.MigrationsAssembly("ApiUserOffice"));
            });


            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = "password@12345#";
            var GetConnectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}";
            builder.Services.AddDbContext<ApiUserOfficeContext>(opt => opt.UseSqlServer(GetConnectionString));

           
            builder.Services.AddScoped<IUserRepositoryDomain, UserRepositoryData>();
            builder.Services.AddScoped<IUserRepositoryDomain, UserRepositoryData>();

            builder.Services.AddScoped<IDepartmentDomain, DepartmentrRepositoryData>();
            builder.Services.AddScoped<IGeralRepositoryDomain, GeralRepositoryData>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}