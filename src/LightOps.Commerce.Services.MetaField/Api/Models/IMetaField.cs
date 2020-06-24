namespace LightOps.Commerce.Services.MetaField.Api.Models
{
    public interface IMetaField
    {
        string ParentEntityType { get; set; }
        string ParentEntityId { get; set; }

        string Name { get; set; }
        string Type { get; set; }
        string Value { get; set; }
    }
}