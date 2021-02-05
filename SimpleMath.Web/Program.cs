using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SimpleMath.Web
{
    public static class Program
    {
        private static void Main(string[] args) =>
            CreateHost(args).Run();

        public static IHost CreateHost(params string[] startupArgs) =>
            CreateHostBuilder(startupArgs).Build();

        public static IHostBuilder CreateHostBuilder(string[] args, Action<IApplicationBuilder> appModifier = null) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(builderContext =>
                    new AutofacServiceProviderFactory(builder =>
                        builder.Properties.Add(
                            WebModule.Configuration,
                            builderContext.Configuration.GetSection("Constants")
                        )))
                .ConfigureWebHostDefaults(builder =>
                    builder.UseStartup(context =>
                        new Startup(context.Configuration, appModifier)));
    }
}