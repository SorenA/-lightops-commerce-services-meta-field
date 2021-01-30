using System;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.MetaField.Api.CommandHandlers;
using LightOps.Commerce.Services.MetaField.Api.Commands;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Domain.CommandHandlers
{
    public class DeleteMetaFieldCommandHandler : IDeleteMetaFieldCommandHandler
    {
        private readonly IInMemoryMetaFieldProvider _inMemoryMetaFieldProvider;

        public DeleteMetaFieldCommandHandler(IInMemoryMetaFieldProvider inMemoryMetaFieldProvider)
        {
            _inMemoryMetaFieldProvider = inMemoryMetaFieldProvider;
        }

        public Task HandleAsync(DeleteMetaFieldCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Id))
            {
                throw new ArgumentException("ID missing.");
            }

            // Check if entity already exists
            var entity = _inMemoryMetaFieldProvider
                .MetaFields?
                .FirstOrDefault(x => x.Id == command.Id);

            // Delete old if found
            if (entity != null)
            {
                _inMemoryMetaFieldProvider.MetaFields?.Remove(entity);
            }

            return Task.CompletedTask;
        }
    }
}