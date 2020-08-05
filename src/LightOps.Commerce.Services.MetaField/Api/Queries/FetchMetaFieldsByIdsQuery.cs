using System.Collections.Generic;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.MetaField.Api.Queries
{
    public class FetchMetaFieldsByIdsQuery : IQuery
    {
        /// <summary>
        /// The ids of the meta-fields requested
        /// </summary>
        public IList<string> Ids { get; set; }
    }
}