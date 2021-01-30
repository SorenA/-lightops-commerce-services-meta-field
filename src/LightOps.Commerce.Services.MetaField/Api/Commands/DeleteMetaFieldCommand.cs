using LightOps.CQRS.Api.Commands;

namespace LightOps.Commerce.Services.MetaField.Api.Commands
{
    public class DeleteMetaFieldCommand : ICommand
    {
        /// <summary>
        /// The id of the meta-field to delete
        /// </summary>
        public string Id { get; set; }
    }
}