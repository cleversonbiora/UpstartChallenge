using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using UpStart.Domain.AutoMapper;
using UpStart.Domain.Interfaces.Api;
using UpStart.Infra.IoC;
using UpStart.Middlewares;

namespace UpStart
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables();

            environment = env;

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            Configuration.GetSection("AppConfiguration").GetChildren().ToList().ForEach(config => Environment.SetEnvironmentVariable(config.Key, config.Value));

            var baseRefitSettings = new RefitSettings(new NewtonsoftJsonContentSerializer());

            services.AddRefitClient<IWeatherApi>(baseRefitSettings)
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("WeatherApi"));
                c.DefaultRequestHeaders.Add("User-Agent", "cleversonbiora");
            });

            services.AddRefitClient<IGeocodingApi>(baseRefitSettings)
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("GeocodingApi"));
            });

            services.AddAutoMapper(conf => AutoMapperConfiguration.RegisterMappings(conf), typeof(Startup).Assembly);
            Mapper.Initialize();
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"{environment.ApplicationName} {environment.EnvironmentName}",
                    Version = "v1",
                    Description = ""
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });

         
            services.AddCors(o => o.AddPolicy("Cors", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            NativeInjectorBootStrapper.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "My API V1"); //originally "./swagger/v1/swagger.json"
            });

            app.UseResponseExceptionHandler();

            app.UseSwagger();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
