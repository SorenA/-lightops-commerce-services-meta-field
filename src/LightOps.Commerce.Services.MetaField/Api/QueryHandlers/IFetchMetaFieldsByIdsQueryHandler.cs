using System.Collections.Generic;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.MetaField.Api.QueryHandlers
{
    public interface IFetchMetaFieldsByIdsQueryHandler : IQueryHandler<FetchMetaFieldsByIdsQuery, IList<Proto.Types.MetaField>>
    {

    }
}