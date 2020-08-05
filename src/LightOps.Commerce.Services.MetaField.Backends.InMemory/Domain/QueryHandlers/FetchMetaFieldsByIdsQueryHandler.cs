using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.Commerce.Services.MetaField.Api.QueryHandlers;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Domain.QueryHandlers
{
    public class FetchMetaFieldsByIdsQueryHandler : IFetchMetaFieldsByIdsQueryHandler
    {
        private readonly IInMemoryMetaFieldProvider _inMemoryMetaFieldProvider;

        public FetchMetaFieldsByIdsQueryHandler(IInMemoryMetaFieldProvider inMemoryMetaFieldProvider)
        {
            _inMemoryMetaFieldProvider = inMemoryMetaFieldProvider;
        }

        public Task<IList<IMetaField>> HandleAsync(FetchMetaFieldsByIdsQuery query)
        {
            var metaFields = _inMemoryMetaFieldProvider
                .MetaFields
                .Where(c => query.Ids.Contains(c.Id))
                .ToList();

            return Task.FromResult<IList<IMetaField>>(metaFields);
        }
    }
    public class FetchMetaFieldsBySearchQueryHandler : IFetchMetaFieldsBySearchQueryHandler
    {
        private readonly IInMemoryMetaFieldProvider _inMemoryMetaFieldProvider;

        public FetchMetaFieldsBySearchQueryHandler(IInMemoryMetaFieldProvider inMemoryMetaFieldProvider)
        {
            _inMemoryMetaFieldProvider = inMemoryMetaFieldProvider;
        }

        public Task<IList<IMetaField>> HandleAsync(FetchMetaFieldsBySearchQuery query)
        {
            var inMemoryQuery = _inMemoryMetaFieldProvider.MetaFields
                .AsQueryable();

            // Match parent id if requested
            if (!string.IsNullOrEmpty(query.ParentId))
            {
                inMemoryQuery = inMemoryQuery.Where(x => x.ParentId == query.ParentId);
            }

            // Match namespace if requested
            if (!string.IsNullOrEmpty(query.Namespace))
            {
                var @namespace = query.Namespace.ToLowerInvariant();

                inMemoryQuery = inMemoryQuery
                    .Where(x =>
                        !string.IsNullOrWhiteSpace(x.Namespace) && x.Namespace.ToLowerInvariant() == @namespace);
            }

            // Match name if requested
            if (!string.IsNullOrEmpty(query.Name))
            {
                var name = query.Name.ToLowerInvariant();

                inMemoryQuery = inMemoryQuery
                    .Where(x =>
                        !string.IsNullOrWhiteSpace(x.Name) || x.Name.ToLowerInvariant() == name);
            }

            var results = inMemoryQuery
                .ToList();

            return Task.FromResult<IList<IMetaField>>(results);
        }
    }
}