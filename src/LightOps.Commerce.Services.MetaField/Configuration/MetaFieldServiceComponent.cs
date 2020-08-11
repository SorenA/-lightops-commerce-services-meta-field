using System.Collections.Generic;
using System.Linq;
using LightOps.Commerce.Proto.Types;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.Commerce.Services.MetaField.Api.QueryHandlers;
using LightOps.Commerce.Services.MetaField.Api.Services;
using LightOps.Commerce.Services.MetaField.Domain.Mappers;
using LightOps.Commerce.Services.MetaField.Domain.Services;
using LightOps.CQRS.Api.Queries;
using LightOps.DependencyInjection.Api.Configuration;
using LightOps.DependencyInjection.Domain.Configuration;
using LightOps.Mapping.Api.Mappers;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LightOps.Commerce.Services.MetaField.Configuration
{
    public class MetaFieldServiceComponent : IDependencyInjectionComponent, IMetaFieldServiceComponent
    {
        public string Name => "lightops.commerce.services.meta-field";

        public IReadOnlyList<ServiceRegistration> GetServiceRegistrations()
        {
            return new List<ServiceRegistration>()
                .Union(_services.Values)
                .Union(_mappers.Values)
                .Union(_queryHandlers.Values)
                .ToList();
        }

        #region Services
        internal enum Services
        {
            HealthService,
            MetaFieldService,
        }

        private readonly Dictionary<Services, ServiceRegistration> _services = new Dictionary<Services, ServiceRegistration>
        {
            [Services.HealthService] = ServiceRegistration.Transient<IHealthService, HealthService>(),
            [Services.MetaFieldService] = ServiceRegistration.Transient<IMetaFieldService, MetaFieldService>(),
        };

        public IMetaFieldServiceComponent OverrideHealthService<T>()
            where T : IHealthService
        {
            _services[Services.HealthService].ImplementationType = typeof(T);
            return this;
        }

        public IMetaFieldServiceComponent OverrideMetaFieldService<T>()
            where T : IMetaFieldService
        {
            _services[Services.MetaFieldService].ImplementationType = typeof(T);
            return this;
        }
        #endregion Services

        #region Mappers
        internal enum Mappers
        {
            MetaFieldProtoMapper,
        }

        private readonly Dictionary<Mappers, ServiceRegistration> _mappers = new Dictionary<Mappers, ServiceRegistration>
        {
            [Mappers.MetaFieldProtoMapper] = ServiceRegistration.Transient<IMapper<IMetaField, MetaFieldProto>, MetaFieldProtoMapper>(),
        };

        public IMetaFieldServiceComponent OverrideMetaFieldProtoMapper<T>() where T : IMapper<IMetaField, MetaFieldProto>
        {
            _mappers[Mappers.MetaFieldProtoMapper].ImplementationType = typeof(T);
            return this;
        }
        #endregion Mappers

        #region Query Handlers
        internal enum QueryHandlers
        {
            CheckMetaFieldHealthQueryHandler,
            FetchMetaFieldsByIdsQueryHandler,
            FetchMetaFieldsByParentIdsQueryHandler,
            FetchMetaFieldsBySearchQueryHandler,
        }

        private readonly Dictionary<QueryHandlers, ServiceRegistration> _queryHandlers = new Dictionary<QueryHandlers, ServiceRegistration>
        {
            [QueryHandlers.CheckMetaFieldHealthQueryHandler] = ServiceRegistration.Transient<IQueryHandler<CheckMetaFieldHealthQuery, HealthStatus>>(),
            [QueryHandlers.FetchMetaFieldsByIdsQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchMetaFieldsByIdsQuery, IList<IMetaField>>>(),
            [QueryHandlers.FetchMetaFieldsByParentIdsQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchMetaFieldsByParentIdsQuery, IDictionary<string, IList<IMetaField>>>>(),
            [QueryHandlers.FetchMetaFieldsBySearchQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchMetaFieldsBySearchQuery, IList<IMetaField>>>(),
        };

        public IMetaFieldServiceComponent OverrideCheckMetaFieldHealthQueryHandler<T>() where T : ICheckMetaFieldHealthQueryHandler
        {
            _queryHandlers[QueryHandlers.CheckMetaFieldHealthQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IMetaFieldServiceComponent OverrideFetchMetaFieldsByIdsQueryHandler<T>() where T : IFetchMetaFieldsByIdsQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchMetaFieldsByIdsQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IMetaFieldServiceComponent OverrideFetchMetaFieldsByParentIdsQueryHandler<T>() where T : IFetchMetaFieldsByParentIdsQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchMetaFieldsByParentIdsQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IMetaFieldServiceComponent OverrideFetchMetaFieldsBySearchQueryHandler<T>() where T : IFetchMetaFieldsBySearchQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchMetaFieldsBySearchQueryHandler].ImplementationType = typeof(T);
            return this;
        }
        #endregion Query Handlers
    }
}