using System.Collections.Generic;
using LightOps.Commerce.Services.MetaField.Api.Models;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Configuration
{
    public interface IInMemoryMetaFieldServiceBackendComponent
    {
        #region Entities
        IInMemoryMetaFieldServiceBackendComponent UseMetaFields(IList<IMetaField> metaFields);
        #endregion Entities
    }
}