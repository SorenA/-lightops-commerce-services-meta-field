using System.Collections.Generic;
using System.Threading.Tasks;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.Commerce.Services.MetaField.Api.Services;
using LightOps.CQRS.Api.Services;

namespace LightOps.Commerce.Services.MetaField.Domain.Services
{
    public class MetaFieldService : IMetaFieldService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public MetaFieldService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }
        
        public Task<IList<IMetaField>> GetByIdAsync(IList<string> ids)
        {
            return _queryDispatcher.DispatchAsync<FetchMetaFieldsByIdsQuery, IList<IMetaField>>(new FetchMetaFieldsByIdsQuery
            {
                Ids = ids,
            });
        }

        public Task<IDictionary<string, IList<IMetaField>>> GetByParentIdsAsync(IList<string> parentIds)
        {
            return _queryDispatcher.DispatchAsync<FetchMetaFieldsByParentIdsQuery, IDictionary<string, IList<IMetaField>>>(new FetchMetaFieldsByParentIdsQuery
            {
                ParentIds = parentIds,
            });
        }

        public Task<IList<IMetaField>> GetBySearchAsync(string parentId, string @namespace, string name)
        {
            return _queryDispatcher.DispatchAsync<FetchMetaFieldsBySearchQuery, IList<IMetaField>>(new FetchMetaFieldsBySearchQuery
            {
                ParentId = parentId,
                Namespace = @namespace,
                Name = name,
            });
        }
    }
}