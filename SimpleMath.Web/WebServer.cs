using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SimpleMath.Web
{
    public static class WebServer
    {
        public static IHost CreateHost(params string[] startupArgs)
        {
            return CreateHostBuilder(startupArgs).Build();
        }

        public static void Main(string[] args) =>
            CreateHostBuilder(args)
                .Build()
                .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((context, services) =>
                {
                    var kestrelConfiguration = context.Configuration.GetSection("Kestrel");
                    services.Configure<KestrelServerOptions>(kestrelConfiguration);
                })
                .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
    }
}