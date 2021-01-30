using System.Collections.Generic;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Domain.Providers
{
    public class InMemoryMetaFieldProvider : IInMemoryMetaFieldProvider
    {
        public IList<Proto.Types.MetaField> MetaFields { get; internal set; } = new List<Proto.Types.MetaField>();
    }
}