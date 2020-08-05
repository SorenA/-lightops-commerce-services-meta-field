using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.MetaField.Api.Queries
{
    public class FetchMetaFieldsBySearchQuery : IQuery
    {
        /// <summary>
        /// Globally unique identifier of parent to search in meta-fields of
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Search only in meta-fields with a specific namespace, if any specified
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Search only in meta-fields with a specific name, if any specified
        /// </summary>
        public string Name { get; set; }
    }
}