using Autofac;
using SimpleMath.Web.SettingsProviders;

namespace SimpleMath.Web
{
    public class WebModule : Module
    {
        public const string Configuration = "Configuration";

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterTypes(ThisAssembly.GetTypes())
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ConfigurationSettingsProvider>()
                .AsImplementedInterfaces()
                .WithParameter("configuration", builder.Properties[Configuration]!);
        }
    }
}