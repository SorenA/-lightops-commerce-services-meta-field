using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.Commerce.Services.MetaField.Api.QueryHandlers;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Domain.QueryHandlers
{
    public class FetchMetaFieldByParentQueryHandler : IFetchMetaFieldByParentQueryHandler
    {
        private readonly IInMemoryMetaFieldProvider _inMemoryMetaFieldProvider;

        public FetchMetaFieldByParentQueryHandler(IInMemoryMetaFieldProvider inMemoryMetaFieldProvider)
        {
            _inMemoryMetaFieldProvider = inMemoryMetaFieldProvider;
        }
        
        public Task<IMetaField> HandleAsync(FetchMetaFieldByParentQuery query)
        {
            var metaField = _inMemoryMetaFieldProvider
                .MetaFields
                .FirstOrDefault(c =>
                    c.ParentEntityType == query.ParentEntityType
                    && c.ParentEntityId == query.ParentEntityId
                    && c.Name == query.Name);

            return Task.FromResult(metaField);
        }
    }
}