using System.Collections.Generic;
using System.Linq;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Domain.Providers;
using LightOps.DependencyInjection.Api.Configuration;
using LightOps.DependencyInjection.Domain.Configuration;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Configuration
{
    public class InMemoryMetaFieldServiceBackendComponent : IDependencyInjectionComponent, IInMemoryMetaFieldServiceBackendComponent
    {
        public string Name => "lightops.commerce.services.meta-field.backend.in-memory";

        public IReadOnlyList<ServiceRegistration> GetServiceRegistrations()
        {
            // Populate in-memory providers
            _providers[Providers.InMemoryMetaFieldProvider].ImplementationInstance = new InMemoryMetaFieldProvider
            {
                MetaFields = _metaFields,
            };

            return new List<ServiceRegistration>()
                .Union(_providers.Values)
                .ToList();
        }

        #region Entities
        private readonly IList<IMetaField> _metaFields = new List<IMetaField>();

        public IInMemoryMetaFieldServiceBackendComponent UseMetaFields(IList<IMetaField> metaFields)
        {
            foreach (var metaField in metaFields)
            {
                _metaFields.Add(metaField);
            }

            return this;
        }
        #endregion Entities

        #region Providers
        internal enum Providers
        {
            InMemoryMetaFieldProvider,
        }

        private readonly Dictionary<Providers, ServiceRegistration> _providers = new Dictionary<Providers, ServiceRegistration>()
        {
            [Providers.InMemoryMetaFieldProvider] = ServiceRegistration.Singleton<IInMemoryMetaFieldProvider>(),
        };
        #endregion Providers
    }
}