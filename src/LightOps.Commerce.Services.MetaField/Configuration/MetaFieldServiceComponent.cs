using System.Collections.Generic;
using System.Linq;
using LightOps.Commerce.Services.MetaField.Api.CommandHandlers;
using LightOps.Commerce.Services.MetaField.Api.Commands;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.Commerce.Services.MetaField.Api.QueryHandlers;
using LightOps.CQRS.Api.Commands;
using LightOps.CQRS.Api.Queries;
using LightOps.DependencyInjection.Api.Configuration;
using LightOps.DependencyInjection.Domain.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LightOps.Commerce.Services.MetaField.Configuration
{
    public class MetaFieldServiceComponent : IDependencyInjectionComponent, IMetaFieldServiceComponent
    {
        public string Name => "lightops.commerce.services.meta-field";

        public IReadOnlyList<ServiceRegistration> GetServiceRegistrations()
        {
            return new List<ServiceRegistration>()
                .Union(_queryHandlers.Values)
                .Union(_commandHandlers.Values)
                .ToList();
        }

        #region Query Handlers
        internal enum QueryHandlers
        {
            CheckMetaFieldServiceHealthQueryHandler,
            FetchMetaFieldsByIdsQueryHandler,
            FetchMetaFieldsByParentIdsQueryHandler,
            FetchMetaFieldsBySearchQueryHandler,
        }

        private readonly Dictionary<QueryHandlers, ServiceRegistration> _queryHandlers = new Dictionary<QueryHandlers, ServiceRegistration>
        {
            [QueryHandlers.CheckMetaFieldServiceHealthQueryHandler] = ServiceRegistration.Transient<IQueryHandler<CheckMetaFieldServiceHealthQuery, HealthStatus>>(),
            [QueryHandlers.FetchMetaFieldsByIdsQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchMetaFieldsByIdsQuery, IList<Proto.Types.MetaField>>>(),
            [QueryHandlers.FetchMetaFieldsByParentIdsQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchMetaFieldsByParentIdsQuery, IDictionary<string, IList<Proto.Types.MetaField>>>>(),
            [QueryHandlers.FetchMetaFieldsBySearchQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchMetaFieldsBySearchQuery, IList<Proto.Types.MetaField>>>(),
        };

        public IMetaFieldServiceComponent OverrideCheckMetaFieldServiceHealthQueryHandler<T>() where T : ICheckMetaFieldServiceHealthQueryHandler
        {
            _queryHandlers[QueryHandlers.CheckMetaFieldServiceHealthQueryHandler].ImplementationType = typeof(T);
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

        #region Command Handlers
        internal enum CommandHandlers
        {
            PersistMetaFieldCommandHandler,
            DeleteMetaFieldCommandHandler,
        }

        private readonly Dictionary<CommandHandlers, ServiceRegistration> _commandHandlers = new Dictionary<CommandHandlers, ServiceRegistration>
        {
            [CommandHandlers.PersistMetaFieldCommandHandler] = ServiceRegistration.Transient<ICommandHandler<PersistMetaFieldCommand>>(),
            [CommandHandlers.DeleteMetaFieldCommandHandler] = ServiceRegistration.Transient<ICommandHandler<DeleteMetaFieldCommand>>(),
        };

        public IMetaFieldServiceComponent OverridePersistMetaFieldCommandHandler<T>() where T : IPersistMetaFieldCommandHandler
        {
            _commandHandlers[CommandHandlers.PersistMetaFieldCommandHandler].ImplementationType = typeof(T);
            return this;
        }

        public IMetaFieldServiceComponent OverrideDeleteMetaFieldCommandHandler<T>() where T : IDeleteMetaFieldCommandHandler
        {
            _commandHandlers[CommandHandlers.DeleteMetaFieldCommandHandler].ImplementationType = typeof(T);
            return this;
        }
        #endregion Command Handlers
    }
}