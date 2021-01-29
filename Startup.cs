using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CoreInvestmentTracker.Models.DAL;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Swagger;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CoreInvestmentTracker
{
    /// <summary>
    /// Startup Class
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Provides us with access to the hosting environment details
        /// </summary>
        private IHostingEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// Startup configuration
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }
        
        /// <summary>
        /// Configuration object
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            
            var mvc = services
                .AddMvc(options => options.EnableEndpointRouting = false )
          .AddNewtonsoftJson(o => 
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllers().AddNewtonsoftJson();


            //mvc.AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});


            // Which database location to connect to?
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var devConnectionString = Configuration.GetConnectionString("SqlExpress2017LocalHostConnection");
                options.UseNpgsql(GetPostgressConnectionString());
                //options.UseSqlServer(!HostingEnvironment.IsDevelopment()
                //    // Connect to amazon RDS database in production
                //    ? GetRdsConnectionString() ?? devConnectionString // fallback to local if cant find rds but not useful really
                //                                                      // Connect to whatever defined database in config file while in development
                //    : devConnectionString);
            });

            // Add application services for dependency injection
            services.AddTransient(typeof(IEntityApplicationDbContext<>), typeof(EntityApplicationDbContext<>));
            services.AddTransient(typeof(IMyLogger), typeof(MyLogger));

            // Configure authentication

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                        //                        ValidateIssuer = true,
                        //                        ValidateAudience = true,
                        //                        ValidateLifetime = true,
                        //                        ValidateIssuerSigningKey = true,
                        //                        ValidIssuer = Configuration[“Jwt:Issuer”],
                        //                        ValidAudience = Configuration[“Jwt:Audience”],
                        //                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[“Jwt:SecretKey”])),
                        //ClockSkew = TimeSpan.Zero
                    };
                });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Core Investment Tracker API",
                    Description = "Investment tracker is a platform that allows you to track aspects that affect your investments",

                    //TermsOfService = ,
                    //Contact = new Contact { Name = "Stuart Mathews", Email = "", Url = "https://twitter.com/stumathews" },
                    //License = new License { Name = "License information", Url = "https://www.stuartmathews.com" }
                });

                //First we define the security scheme
                c.AddSecurityDefinition("Bearer", //Name the security scheme
                    new OpenApiSecurityScheme{
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                    Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{ 
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });

                //    new Dictionary<string, IEnumerable<string>>
                //{
                //    { "Bearer", new string[] { } }
                //});

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // Set the comments path for the Swagger JSON and UI.
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });


        }
        private static string GetXmlCommentsPath(string appPath, string appName)
        {
            return System.IO.Path.Combine(appPath, System.IO.Path.ChangeExtension(appName, "xml"));
        }

        private string GetRdsConnectionString()
        {
            var appConfig = Configuration;

            var dbname = appConfig["RDS_DB_NAME"];

            if (string.IsNullOrEmpty(dbname)) return null;
            
            return "Data Source=" + appConfig["RDS_HOSTNAME"] + ";Initial Catalog=" + dbname + ";User ID=" + appConfig["RDS_USERNAME"] + ";Password=" + appConfig["RDS_PASSWORD"] + ";";
        }

        private string GetPostgressConnectionString()
        {
            string GetHerokuPgSqlConnectionString(IConfiguration configuration)
            {
                var uri = configuration["DATABASE_URL"];
                var r = Regex.Match(uri, @"postgres:\/\/(?<user>.+?):(?<password>.+)@(?<host>[^:]+):5432\/(?<db>.+)");
                string host = r.Groups["host"].Value;
                string username = r.Groups["user"].Value;
                string password = r.Groups["password"].Value;
                string database = r.Groups["db"].Value;

                var s = $"Host={host};Port=5432;Username={username};Password={password};Database={database};";
                System.Console.Write($"Connecting to '{s}'");
                return s;
            }

            var appConfig = Configuration;

            var localPgSql = "Host=localhost;Port=5432;Username=postgres;Password=sa;Database=Investments;";

            //var connectionString = localPgSql;
            var connectionString =  string.IsNullOrEmpty(appConfig["DATABASE_URL"]) 
            ? localPgSql
            : GetHerokuPgSqlConnectionString(appConfig);
            return connectionString;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStatusCodePages("text/plain", "Status code page, status code: {0}");
            app.UseCors("CorsPolicy");
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Investment}/{action=Index}/{id?}");
            });


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Investment Tracker API V1");
                
            });

            
        }
    }
}
