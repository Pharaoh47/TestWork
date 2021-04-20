using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Phonebook
{
    // base aspnet core configuration class, we specify here every thing we use on server side
    public class Startup
    {
        // we accept an IConfiguration on construct from Program class ad host builder
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        

        // services configuration goes here, for test project it is minimalistic
        public void ConfigureServices(IServiceCollection services)
        {
            // we use SQLite database, be sure to change path if you on Windows
            services.AddDbContext<PhonebookContext>(options => { options.UseSqlite($"Data Source=/tmp/phonebook.db"); });   
            // we use MVC controllers for web api, so add it
            services.AddControllers();
            // an Swagger used to show CRUD summary
            services.AddSwaggerGen();
            // and razor page (single) we use to initiate SPA
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Swagger config
            app.UseSwagger();
            app.UseSwaggerUI(c => {c.SwaggerEndpoint("/swagger/v1/swagger.json", "Phonebook API V1");});
            // use developer exception page for test project
            app.UseDeveloperExceptionPage();
            // use https redirection as an web standart
            app.UseHttpsRedirection();
            // use static files for js and css of libraries and for site.js
            app.UseStaticFiles();
            // use routing for web api controllers routes
            app.UseRouting();
            // use endpoints defaults to MVC and Razor
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
