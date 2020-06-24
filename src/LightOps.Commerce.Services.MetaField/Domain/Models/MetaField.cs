using LightOps.Commerce.Services.MetaField.Api.Models;

namespace LightOps.Commerce.Services.MetaField.Domain.Models
{
    public class MetaField : IMetaField
    {
        public string ParentEntityType { get; set; }
        public string ParentEntityId { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}