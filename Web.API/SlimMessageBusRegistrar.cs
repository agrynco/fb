using System.Reflection;
using SlimMessageBus.Host;
using SlimMessageBus.Host.Memory;

namespace Web.API;

public static class SlimMessageBusRegistrar
{
    public static void AddSlimMessageBus(this WebApplicationBuilder builder,
        Assembly[] autoDeclareFromAssemblies, Assembly[] consumersAssemblies)
    {
        builder.Services.AddSlimMessageBus(mbb =>
        {
            mbb.WithProviderMemory(settings => { settings.EnableMessageSerialization = false; })
                .AutoDeclareFrom(autoDeclareFromAssemblies);

            foreach (Assembly consumersAssembly in consumersAssemblies)
            {
                mbb.AddServicesFromAssembly(consumersAssembly);
            }
        });
    }
}