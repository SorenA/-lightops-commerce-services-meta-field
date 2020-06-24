using System.Collections.Generic;
using LightOps.Commerce.Services.MetaField.Api.Models;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers
{
    public interface IInMemoryMetaFieldProvider
    {
        IList<IMetaField> MetaFields { get; }
    }
}