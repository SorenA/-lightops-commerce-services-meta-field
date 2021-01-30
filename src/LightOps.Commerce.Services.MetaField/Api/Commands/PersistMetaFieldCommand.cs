using LightOps.CQRS.Api.Commands;

namespace LightOps.Commerce.Services.MetaField.Api.Commands
{
    public class PersistMetaFieldCommand : ICommand
    {
        /// <summary>
        /// The id of the meta-field to persist
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The meta-field to persist
        /// </summary>
        public Proto.Types.MetaField MetaField { get; set; }
    }
}