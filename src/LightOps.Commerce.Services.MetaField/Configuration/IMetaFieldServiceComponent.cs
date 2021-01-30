using LightOps.Commerce.Services.MetaField.Api.CommandHandlers;
using LightOps.Commerce.Services.MetaField.Api.QueryHandlers;

namespace LightOps.Commerce.Services.MetaField.Configuration
{
    public interface IMetaFieldServiceComponent
    {
        #region Query Handlers
        IMetaFieldServiceComponent OverrideCheckMetaFieldServiceHealthQueryHandler<T>() where T : ICheckMetaFieldServiceHealthQueryHandler;
        IMetaFieldServiceComponent OverrideFetchMetaFieldsByIdsQueryHandler<T>() where T : IFetchMetaFieldsByIdsQueryHandler;
        IMetaFieldServiceComponent OverrideFetchMetaFieldsByParentIdsQueryHandler<T>() where T : IFetchMetaFieldsByParentIdsQueryHandler;
        IMetaFieldServiceComponent OverrideFetchMetaFieldsBySearchQueryHandler<T>() where T : IFetchMetaFieldsBySearchQueryHandler;
        #endregion Query Handlers

        #region Command Handlers
        IMetaFieldServiceComponent OverridePersistMetaFieldCommandHandler<T>() where T : IPersistMetaFieldCommandHandler;
        IMetaFieldServiceComponent OverrideDeleteMetaFieldCommandHandler<T>() where T : IDeleteMetaFieldCommandHandler;
        #endregion Command Handlers
    }
}