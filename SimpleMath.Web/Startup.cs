using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleMath.Web.Middleware;

namespace SimpleMath.Web
{
    public class Startup
    {
        private readonly Action<IApplicationBuilder> appModifier;

        public Startup(IConfiguration configuration, Action<IApplicationBuilder> appModifier = null)
        {
            Configuration = configuration;
            this.appModifier = appModifier;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) =>
            services.AddControllers()
                .AddControllersAsServices()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assemblies = LoadAssemblies(AppDomain.CurrentDomain.BaseDirectory, "SimpleMath.*.dll");
            builder.RegisterAssemblyModules(assemblies);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMiddleware<ParallelLimitingMiddleware>();

            appModifier?.Invoke(app);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static Assembly[] LoadAssemblies(string folderPath, string searchPattern) =>
            Directory.EnumerateFiles(folderPath, searchPattern)
                .Select(Assembly.LoadFrom)
                .ToArray();
    }
}