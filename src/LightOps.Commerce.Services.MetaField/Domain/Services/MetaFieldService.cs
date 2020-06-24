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
        
        public Task<IMetaField> GetByParentAsync(string parentEntityType, string parentEntityId, string name)
        {
            return _queryDispatcher.DispatchAsync<FetchMetaFieldByParentQuery, IMetaField>(new FetchMetaFieldByParentQuery
            {
                ParentEntityType = parentEntityType,
                ParentEntityId = parentEntityId,
                Name = name,
            });
        }

        public Task<IList<IMetaField>> GetByParentAsync(string parentEntityType, string parentEntityId)
        {
            return _queryDispatcher.DispatchAsync<FetchMetaFieldsByParentQuery, IList<IMetaField>>(new FetchMetaFieldsByParentQuery
            {
                ParentEntityType = parentEntityType,
                ParentEntityId = parentEntityId,
            });
        }
    }
}