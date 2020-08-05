using System.Collections.Generic;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.MetaField.Api.Queries
{
    public class FetchMetaFieldsByParentIdsQuery : IQuery
    {
        /// <summary>
        /// The parent ids of the meta-fields requested
        /// </summary>
        public IList<string> ParentIds { get; set; }
    }
}