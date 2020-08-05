using LightOps.Commerce.Proto.Types;
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

        #region Mappers
        IMetaFieldServiceComponent OverrideMetaFieldProtoMapper<T>() where T : IMapper<IMetaField, MetaFieldProto>;
        #endregion Mappers

        #region Query Handlers
        IMetaFieldServiceComponent OverrideCheckMetaFieldHealthQueryHandler<T>() where T : ICheckMetaFieldHealthQueryHandler;
        IMetaFieldServiceComponent OverrideFetchMetaFieldsByIdsQueryHandler<T>() where T : IFetchMetaFieldsByIdsQueryHandler;
        IMetaFieldServiceComponent OverrideFetchMetaFieldsByParentIdsQueryHandler<T>() where T : IFetchMetaFieldsByParentIdsQueryHandler;
        IMetaFieldServiceComponent OverrideFetchMetaFieldsBySearchQueryHandler<T>() where T : IFetchMetaFieldsBySearchQueryHandler;
        #endregion Query Handlers
    }
}