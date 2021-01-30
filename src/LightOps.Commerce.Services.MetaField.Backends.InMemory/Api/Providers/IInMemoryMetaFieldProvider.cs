using System.Collections.Generic;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers
{
    public interface IInMemoryMetaFieldProvider
    {
        IList<Proto.Types.MetaField> MetaFields { get; }
    }
}