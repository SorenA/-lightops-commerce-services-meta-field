using System.Threading.Tasks;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.Commerce.Services.MetaField.Api.Services;
using LightOps.CQRS.Api.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LightOps.Commerce.Services.MetaField.Domain.Services
{
    public class HealthService : IHealthService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public HealthService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public Task<HealthStatus> CheckMetaField()
        {
            return _queryDispatcher.DispatchAsync<CheckMetaFieldHealthQuery, HealthStatus>(new CheckMetaFieldHealthQuery());
        }
    }
}