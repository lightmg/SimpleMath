using Autofac;

namespace SimpleMath.BusinessLogic
{
    public class BusinessLogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterTypes(ThisAssembly.GetTypes())
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}