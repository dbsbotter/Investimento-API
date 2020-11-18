using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Investimento.Api.Convetions;
using Investimento.Api.Filters;
using Investimento.Domain.Exceptions;
using Investimento.Domain.Handlers.Contracts;
using Investimento.Domain.Infra.Contexts;
using Investimento.Domain.Infra.Repositories;
using Investimento.Domain.Repositories;
using Investimento.Domain.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Investimento.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomDefaultAspNetServices(Configuration);
            services.AddCustomSwaggerDoc(Configuration, Env);
            services.AddProblemDetails(x =>
            {
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });
            services.AddHttpContextAccessor();

            services.AddHttpClient("AlphaVantage", options =>
            {
                options.BaseAddress = new Uri("https://www.alphavantage.co");
            });

            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("conn")));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();

            app.UseResponseCompression();

            app.UseCustomSwaggerDoc(Configuration, Env);

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    static class CustomServicesConfiguration
    {
        public static IServiceCollection AddCustomDefaultAspNetServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.AddHttpContextAccessor();
            services.AddDefaultCors();
            services.AddResponseCompression();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Type = $"https://httpstatuses.com/400",
                        Title = "Dados inválidos",
                        Detail = "Os dados enviados na requisição não estão corretos"
                    };

                    return new BadRequestObjectResult(problemDetails);
                };
            });

            services.Configure<JsonOptions>(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.Configure<MvcOptions>(opt =>
            {
                opt.UseApiVersionRoute();
            });

            services
                .AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());

            services.AddDependencyInjections();

            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Auth:SecretKey"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection AddCustomSwaggerDoc(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            var enbaleSwaggerProd = configuration.GetValue<bool>("Docs:EnableSwaggerProd");

            if (env.IsDevelopment() || enbaleSwaggerProd)
            {
                services.AddSwaggerGen(options =>
                {
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                        {
                            Title = "API Investimento",
                            Version = description.ApiVersion.ToString()
                        });
                    }

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Authorização JWT Header usando o schema Bearer. Exemplo: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

                    options.IncludeXmlComments(xmlPath);
                });

                services.AddSwaggerGenNewtonsoftSupport();
            }

            return services;
        }
    }

    static class CustomApplicationConfiguration
    {
        public static IApplicationBuilder UseCustomSwaggerDoc(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
        {
            var enbaleSwaggerProd = configuration.GetValue<bool>("Docs:EnableSwaggerProd");

            if (env.IsDevelopment() || enbaleSwaggerProd)
            {
                var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                //Configura Swagger
                app.UseSwagger(options =>
                {
                    options.RouteTemplate = "swagger/{documentName}/swagger.json";


                });

                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = "swagger";

                    // Cria um endpoint Swagger para cada ApiVersion: Ex: [ApiVersion("1.0")]
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"{description.GroupName}/swagger.json", $"Versao {description.GroupName.ToUpperInvariant()}");
                    }
                });
            }

            return app;
        }
    }

    static class CorsServiceCollectionExtensions
    {
        /// <summary>
        /// Configura opção default para CORS
        /// <param name="services">Contrato para o pipeline de injeção de dependência</param>
        /// </summary>
        public static IServiceCollection AddDefaultCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowAnyOrigin();
                    //.WithOrigins(origins);
                });
            });

            return services;
        }
    }

    static class RouteConventionExtensions
    {
        /// <summary>
        /// Inclui o versionamento 'v{version:apiVersion}' nas rotas de todos os controllers.
        /// </summary>
        public static void UseApiVersionRoute(this MvcOptions options)
        {
            string routeTemplate = "api/v{version:apiVersion}";

            options.Conventions.Insert(0, new ApiVersionRouteConvention(routeTemplate));
        }
    }

    static class DependencyInjectionExtension
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddHandlers();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IInvestimentRepository, InvestimentRepository>();
        }

        public static void AddHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            var commandHandlers = typeof(IHandler<,>)
                .Assembly
                .GetTypes()
                .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<,>)));

            foreach (var handler in commandHandlers)
            {
                services.AddTransient(handler);
            }
        }
    }
}
