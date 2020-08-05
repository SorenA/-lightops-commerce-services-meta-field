using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.Commerce.Services.MetaField.Api.QueryHandlers;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Domain.QueryHandlers
{
    public class FetchMetaFieldsByParentIdsQueryHandler : IFetchMetaFieldsByParentIdsQueryHandler
    {
        private readonly IInMemoryMetaFieldProvider _inMemoryMetaFieldProvider;

        public FetchMetaFieldsByParentIdsQueryHandler(IInMemoryMetaFieldProvider inMemoryMetaFieldProvider)
        {
            _inMemoryMetaFieldProvider = inMemoryMetaFieldProvider;
        }

        public Task<IDictionary<string, IList<IMetaField>>> HandleAsync(FetchMetaFieldsByParentIdsQuery query)
        {
            var metaFieldMap = _inMemoryMetaFieldProvider
                .MetaFields
                .Where(c => query.ParentIds.Contains(c.ParentId))
                .GroupBy(x => x.ParentId)
                .ToDictionary(
                    k => k.Key,
                    v => (IList<IMetaField>)v.ToList());

            return Task.FromResult<IDictionary<string, IList<IMetaField>>>(metaFieldMap);
        }
    }
}