using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.CQRS.Api.Queries;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LightOps.Commerce.Services.MetaField.Api.QueryHandlers
{
    public interface ICheckMetaFieldHealthQueryHandler : IQueryHandler<CheckMetaFieldHealthQuery, HealthStatus>
    {

    }
}