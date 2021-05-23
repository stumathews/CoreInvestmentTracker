using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Support
{
    public class Swagger
    {
        private static string GetXmlCommentsPath(string appPath, string appName)
        {
            return System.IO.Path.Combine(appPath, System.IO.Path.ChangeExtension(appName, "xml"));
        }

        internal static void ConfigureSwagger(IServiceCollection services)
        {
            // Configure Swagger
            services.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Core Investment Tracker API",
                    Description = "Investment tracker is a platform that allows you to track aspects that affect your investments",                    
                    TermsOfService = new Uri("http://www.stuartmathews.com"),
                    Contact = new OpenApiContact { Name = "Stuart Mathews", Email = "", Url = new Uri("https://twitter.com/stumathews") },
                    License = new OpenApiLicense { Name = "License information", Url = new Uri("https://www.stuartmathews.com") }
                });

                swaggerOptions.UseAllOfToExtendReferenceSchemas();

                swaggerOptions.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme{
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement{ 
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                
                swaggerOptions.IncludeXmlComments(xmlPath); // Set the comments path for the Swagger JSON and UI.
                
            });
        }
    }

}