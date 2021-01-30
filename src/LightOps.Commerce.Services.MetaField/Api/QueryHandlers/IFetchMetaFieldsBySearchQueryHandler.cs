using System.Collections.Generic;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.MetaField.Api.QueryHandlers
{
    public interface IFetchMetaFieldsBySearchQueryHandler : IQueryHandler<FetchMetaFieldsBySearchQuery, IList<Proto.Types.MetaField>>
    {

    }
}