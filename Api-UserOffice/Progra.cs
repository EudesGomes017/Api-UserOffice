using Api_UserOffice.filter.api;
using Data;
using Data.Context;
using Data.RepositoryData;
using Domain.Integracao.Interface;
using Domain.Integracao.Refit;
using Domain.Integracao.Service;
using Domain.Interface.RepositoryDomain;
using Domain.Services.serviceUser.AuthUser;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Services.serviceUser.loggedInUser;
using Domain.Services.serviceUser.services;
using Domain.Services.serviceUser.services.SharedUser;
using Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;
using System.Text.Json.Serialization;

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


            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddApplication(builder.Configuration);

            builder.Services.AddHttpContextAccessor(); // 


            if (builder.Configuration.GetValue<bool>("ConfigurationMemory:BancoDeDadosInMemory"))
            {
                builder.Services.AddDbContext<ApiUserOfficeContext>(options =>
                {
                    options.UseInMemoryDatabase("ConfigurationMemory");
                });
            }
            else
            {
                builder.Services.AddDbContext<ApiUserOfficeContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiUserOffice"));
                });
            }

            builder.Services.AddMvc(optins => optins.Filters.Add(typeof(FilterExcepetion)));

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = "password@12345#";
            var GetConnectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}";

            builder.Services.AddDbContext<ApiUserOfficeContext>(opt => opt.UseSqlServer(GetConnectionString));

            builder.Services.AddScoped<IPostUser, PostUser>();
            builder.Services.AddScoped<ISearchEamil, UserEamil>();

            builder.Services.AddScoped<IVerifyDocument, VerificarDocumento>();
            builder.Services.AddScoped<IloggedInUser, loggedInUser>();

            builder.Services.AddScoped<IGetUserRepositoryDomainDto, GetUser>();
            builder.Services.AddScoped<IUserUp, UserUp>();

            builder.Services.AddScoped<IDeleteUser, DeleteUser>();
            builder.Services.AddScoped<ILoginUser, LoginUser>();

            builder.Services.AddScoped<IGetUserRepositoryDomain, UserRepositoryData>();
            builder.Services.AddScoped<IVerifyPassWord, VerificaPassWord>();

            builder.Services.AddScoped<IDepartmentDomain, DepartmentrRepositoryData>();
            builder.Services.AddScoped<IGeralRepositoryDomain, GeralRepositoryData>();

            builder.Services.AddScoped<INewPassword, NewPassword>();
            builder.Services.AddScoped<Domain.Integracao.Interface.IViaCepIntegracao, ViaCepIntegracao>();

            builder.Services.AddRefitClient<IViacepRefit>().ConfigureHttpClient(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://viacep.com.br/");
            });

            // Add services to the container.
            builder.Services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                   .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling =
                       Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                   .AddNewtonsoftJson(opt => opt.SerializerSettings.NullValueHandling =
                       Newtonsoft.Json.NullValueHandling.Ignore)
                   .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            builder.Services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SimtricKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserOffice.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\nExample: \"Bearer 12345abcdef",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
            });


            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //    options.JsonSerializerOptions.IgnoreNullValues = true;
            //    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //});


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddMvc(optins => optins.Filters.Add(typeof(FilterExcepetion)));
            builder.Services.AddScoped<UserAuthentication>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserOffice.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
        static SymmetricSecurityKey SimtricKey()
        {
            var symmetricKey = Convert.FromBase64String("MihAQDFIZjE8JSx8TVhaSTRlIXRxTUhBVlkoRGFD");
            return new SymmetricSecurityKey(symmetricKey);
        }
    }

}
