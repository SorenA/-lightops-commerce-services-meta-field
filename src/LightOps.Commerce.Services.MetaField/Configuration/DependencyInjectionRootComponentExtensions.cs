using System;
using LightOps.DependencyInjection.Configuration;

namespace LightOps.Commerce.Services.MetaField.Configuration
{
    public static class DependencyInjectionRootComponentExtensions
    {

        public static IDependencyInjectionRootComponent AddMetaFieldService(this IDependencyInjectionRootComponent rootComponent, Action<IMetaFieldServiceComponent> componentConfig = null)
        {
            var component = new MetaFieldServiceComponent();

            // Invoke config delegate
            componentConfig?.Invoke(component);

            // Attach to root component
            rootComponent.AttachComponent(component);

            return rootComponent;
        }
    }
}
