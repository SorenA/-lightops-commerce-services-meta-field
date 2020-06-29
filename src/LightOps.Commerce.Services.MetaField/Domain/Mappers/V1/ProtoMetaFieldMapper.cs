using LightOps.Commerce.Proto.Services.MetaField.V1;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Mapping.Api.Mappers;

// ReSharper disable UseObjectOrCollectionInitializer

namespace LightOps.Commerce.Services.MetaField.Domain.Mappers.V1
{
    public class ProtoMetaFieldMapper : IMapper<IMetaField, ProtoMetaField>
    {
        public ProtoMetaField Map(IMetaField source)
        {
            var dest = new ProtoMetaField();

            dest.ParentEntityType = source.ParentEntityType;
            dest.ParentEntityId = source.ParentEntityId;

            dest.Name= source.Name;
            dest.Type = source.Type;
            dest.Value = source.Value;

            return dest;
        }
    }
}
