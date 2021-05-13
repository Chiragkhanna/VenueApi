using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using VenueApi.Interfaces;
using VenueApi.Models;
using VenueApi.Services;

namespace VenueApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           

            // requires using Microsoft.Extensions.Options
            services.Configure<TicketDbDatabaseSettings>(
                Configuration.GetSection(nameof(TicketDbDatabaseSettings)));

            services.AddSingleton<ITicketDbDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TicketDbDatabaseSettings>>().Value);

            services.AddSingleton<IVenueService, VenueService>();
            services.AddHealthChecks();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("0.0.1", new OpenApiInfo
                {
                    Version = "0.0.1",
                    Title = "Tickets API",
                    Description = "Tickets API (ASP.NET Core 3.1)",
                    Contact = new OpenApiContact()
                    {
                        Name = "Swagger Codegen Contributors",
                        Url = new Uri("https://github.com/swagger-api/swagger-codegen"),
                        Email = ""
                    },
                    TermsOfService = new Uri("https://example.com/terms")
                });
            });
            services.AddCors();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseRouting();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapHealthChecks("/health");
            //});
            app
               .UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/0.0.1/swagger.json", "Venue Config API");
                   c.RoutePrefix = string.Empty;
               });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
