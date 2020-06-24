using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.MetaField.Api.Queries
{
    public class FetchMetaFieldByParentQuery : IQuery
    {
        public string ParentEntityType { get; set; }
        public string ParentEntityId { get; set; }

        public string Name { get; set; }
    }
}