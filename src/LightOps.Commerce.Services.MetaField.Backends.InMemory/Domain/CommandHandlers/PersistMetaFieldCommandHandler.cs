using System;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.MetaField.Api.CommandHandlers;
using LightOps.Commerce.Services.MetaField.Api.Commands;
using LightOps.Commerce.Services.MetaField.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.MetaField.Backends.InMemory.Domain.CommandHandlers
{
    public class PersistMetaFieldCommandHandler : IPersistMetaFieldCommandHandler
    {
        private readonly IInMemoryMetaFieldProvider _inMemoryMetaFieldProvider;

        public PersistMetaFieldCommandHandler(IInMemoryMetaFieldProvider inMemoryMetaFieldProvider)
        {
            _inMemoryMetaFieldProvider = inMemoryMetaFieldProvider;
        }

        public Task HandleAsync(PersistMetaFieldCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Id))
            {
                throw new ArgumentException("ID missing.");
            }

            if (command.MetaField.Id != command.Id)
            {
                throw new ArgumentException("Provided ID and entity ID do not match.");
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

            // Add entity to provider
            _inMemoryMetaFieldProvider.MetaFields?.Add(command.MetaField);

            return Task.CompletedTask;
        }
    }
}