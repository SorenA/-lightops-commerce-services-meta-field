using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.MetaField.Api.Queries
{
    public class FetchMetaFieldsByParentQuery : IQuery
    {
        public string ParentEntityType { get; set; }
        public string ParentEntityId { get; set; }
    }
}