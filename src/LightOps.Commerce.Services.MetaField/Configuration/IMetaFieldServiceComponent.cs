using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Commerce.Services.MetaField.Api.QueryHandlers;
using LightOps.Commerce.Services.MetaField.Api.Services;
using LightOps.Mapping.Api.Mappers;

namespace LightOps.Commerce.Services.MetaField.Configuration
{
    public interface IMetaFieldServiceComponent
    {
        #region Services
        IMetaFieldServiceComponent OverrideHealthService<T>() where T : IHealthService;
        IMetaFieldServiceComponent OverrideMetaFieldService<T>() where T : IMetaFieldService;
        #endregion Services

        #region Query Handlers
        IMetaFieldServiceComponent OverrideCheckMetaFieldHealthQueryHandler<T>() where T : ICheckMetaFieldHealthQueryHandler;
        IMetaFieldServiceComponent OverrideIFetchMetaFieldByParentQueryHandler<T>() where T : IFetchMetaFieldByParentQueryHandler;
        IMetaFieldServiceComponent OverrideIFetchMetaFieldsByParentQueryHandler<T>() where T : IFetchMetaFieldsByParentQueryHandler;
        #endregion Query Handlers
    }
}