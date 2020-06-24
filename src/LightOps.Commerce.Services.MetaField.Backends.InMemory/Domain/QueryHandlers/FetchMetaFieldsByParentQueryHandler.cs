using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.Commerce.Services.MetaField.Api.QueryHandlers;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Domain.QueryHandlers
{
    public class FetchMetaFieldsByParentQueryHandler : IFetchMetaFieldsByParentQueryHandler
    {
        private readonly IInMemoryMetaFieldProvider _inMemoryMetaFieldProvider;

        public FetchMetaFieldsByParentQueryHandler(IInMemoryMetaFieldProvider inMemoryMetaFieldProvider)
        {
            _inMemoryMetaFieldProvider = inMemoryMetaFieldProvider;
        }
        
        public Task<IList<IMetaField>> HandleAsync(FetchMetaFieldsByParentQuery query)
        {
            var metaFields = _inMemoryMetaFieldProvider
                .MetaFields
                .Where(c =>
                    c.ParentEntityType == query.ParentEntityType
                    && c.ParentEntityId == query.ParentEntityId)
                .ToList();

            return Task.FromResult<IList<IMetaField>>(metaFields);
        }
    }
}