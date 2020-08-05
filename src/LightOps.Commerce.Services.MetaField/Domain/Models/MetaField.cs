using System;
using LightOps.Commerce.Services.MetaField.Api.Models;

namespace LightOps.Commerce.Services.MetaField.Domain.Models
{
    public class MetaField : IMetaField
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Namespace { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}