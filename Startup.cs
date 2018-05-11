﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CoreInvestmentTracker.Models.DAL;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Common;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;

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
            var mvc = services.AddMvc();
            mvc.AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // Which database location to connect to?
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var devConnectionString = Configuration.GetConnectionString("LocalDBConnection");

                options.UseSqlServer(!HostingEnvironment.IsDevelopment()
                    // Connect to amazon RDS database in production
                    ? GetRdsConnectionString() ?? devConnectionString // fallback to local if cant find rds but not useful really
                    // Connect to whatever defined database in config file while in development
                    : devConnectionString);
            });

            // Add application services for dependency injection
            services.AddTransient(typeof(IEntityApplicationDbContext<>), typeof(EntityApplicationDbContext<>));
            services.AddTransient(typeof(IMyLogger), typeof(MyLogger));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Core Investment Tracker API",
                    Description = "Investment tracker is a platform that allows you to track aspects that affect your investments",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Stuart Mathews", Email = "", Url = "https://twitter.com/stumathews" },
                    License = new License { Name = "License information", Url = "https://www.stuartmathews.com" }
                });

                // Set the comments path for the Swagger JSON and UI.
                c.IncludeXmlComments(GetXmlCommentsPath());
            });

            services.AddCors(options => options.AddPolicy("Cors", builder => {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

        }
        private static string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            return System.IO.Path.Combine(app.ApplicationBasePath, System.IO.Path.ChangeExtension(app.ApplicationName, "xml"));
        }

        private string GetRdsConnectionString()
        {
            var appConfig = Configuration;

            var dbname = appConfig["RDS_DB_NAME"];

            if (string.IsNullOrEmpty(dbname)) return null;
            
            return "Data Source=" + appConfig["RDS_HOSTNAME"] + ";Initial Catalog=" + dbname + ";User ID=" + appConfig["RDS_USERNAME"] + ";Password=" + appConfig["RDS_PASSWORD"] + ";";
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
            
            app.UseStaticFiles();
            app.UseStatusCodePages("text/plain", "Status code page, status code: {0}");
            app.UseCors("Cors");
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
