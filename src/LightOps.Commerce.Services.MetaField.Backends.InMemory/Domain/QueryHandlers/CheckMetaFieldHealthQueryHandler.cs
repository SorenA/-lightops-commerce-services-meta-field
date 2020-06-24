using System.Threading.Tasks;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.Commerce.Services.MetaField.Api.QueryHandlers;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Domain.QueryHandlers
{
    public class CheckMetaFieldHealthQueryHandler : ICheckMetaFieldHealthQueryHandler
    {
        private readonly IInMemoryMetaFieldProvider _inMemoryMetaFieldProvider;

        public CheckMetaFieldHealthQueryHandler(IInMemoryMetaFieldProvider inMemoryMetaFieldProvider)
        {
            _inMemoryMetaFieldProvider = inMemoryMetaFieldProvider;
        }

        public Task<HealthStatus> HandleAsync(CheckMetaFieldHealthQuery query)
        {
            return _inMemoryMetaFieldProvider.MetaFields != null
                ? Task.FromResult(HealthStatus.Healthy)
                : Task.FromResult(HealthStatus.Unhealthy);
        }
    }
}