using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LightOps.Commerce.Services.MetaField.Api.Services
{
    public interface IHealthService
    {
        Task<HealthStatus> CheckMetaField();
    }
}