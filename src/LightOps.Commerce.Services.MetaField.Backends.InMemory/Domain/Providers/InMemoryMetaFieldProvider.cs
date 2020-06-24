using System.Collections.Generic;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Domain.Providers
{
    public class InMemoryMetaFieldProvider : IInMemoryMetaFieldProvider
    {
        public IList<IMetaField> MetaFields { get; internal set; }
    }
}