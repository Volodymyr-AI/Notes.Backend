using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Notes.WebApi
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                var apiVersion = description.ApiVersion.ToString();
                options.SwaggerDoc(description.GroupName,
                    new OpenApiInfo
                    {
                        Version = apiVersion,
                        Title = $"Notes API {apiVersion}",
                        Description = "ASP.Net Core Web API. Using clean code, SOLID, AutoMapper, CQRS, Swagger, IdentityServer",
                        TermsOfService = new Uri("https://www.youtube.com/watch?v=PPKqao0EKd4"), //Glory to Ukraine 
                        Contact = new OpenApiContact
                        {
                            Name = "V.V.M.",
                            Email = string.Empty,
                            Url =
                                new Uri("https://www.youtube.com/watch?v=PPKqao0EKd4")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "No License but we have music",
                            Url = new Uri("https://www.youtube.com/watch?v=PPKqao0EKd4")
                        }

                    });


                options.AddSecurityDefinition($"AuthToken {apiVersion}", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Name = "Authorization",
                    Description = "Authorization token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = $"AuthToken {apiVersion}"
                            }
                        },
                        new string[] { }
                    }
                });

                options.CustomOperationIds(ApiDescription =>
                    ApiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
                            ? methodInfo.Name : null);
            }
        }
    }
}
