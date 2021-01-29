using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
namespace Support
{
    public class Exceptions
    {
        internal static void ConfigureExceptions(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
        }
    }
}