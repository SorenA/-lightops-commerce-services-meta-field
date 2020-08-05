using Google.Protobuf.WellKnownTypes;
using LightOps.Commerce.Proto.Types;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Mapping.Api.Mappers;

namespace LightOps.Commerce.Services.MetaField.Domain.Mappers
{
    public class MetaFieldProtoMapper : IMapper<IMetaField, MetaFieldProto>
    {
        public MetaFieldProto Map(IMetaField src)
        {
            return new MetaFieldProto
            {
                Id = src.Id,
                ParentId = src.ParentId,
                Namespace = src.Namespace,
                Name = src.Name,
                Type = src.Type,
                Value = src.Value,
                CreatedAt = Timestamp.FromDateTime(src.CreatedAt.ToUniversalTime()),
                UpdatedAt = Timestamp.FromDateTime(src.UpdatedAt.ToUniversalTime()),
            };
        }
    }
}
