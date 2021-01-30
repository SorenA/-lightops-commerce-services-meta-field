using System.Collections.Generic;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Configuration
{
    public interface IInMemoryMetaFieldServiceBackendComponent
    {
        #region Entities
        IInMemoryMetaFieldServiceBackendComponent UseMetaFields(IList<Proto.Types.MetaField> metaFields);
        #endregion Entities

        #region Providers
        IInMemoryMetaFieldServiceBackendComponent OverrideMetaFieldProvider<T>() where T : IInMemoryMetaFieldProvider;
        #endregion Providers
    }
}