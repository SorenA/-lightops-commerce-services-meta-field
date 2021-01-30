using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using LightOps.Commerce.Proto.Services.MetaField;
using LightOps.Commerce.Services.MetaField.Api.Commands;
using LightOps.Commerce.Services.MetaField.Api.Queries;
using LightOps.CQRS.Api.Services;
using Microsoft.Extensions.Logging;

namespace LightOps.Commerce.Services.MetaField.Domain.GrpcServices
{
    public class MetaFieldGrpcService : MetaFieldService.MetaFieldServiceBase
    {
        private readonly ILogger<MetaFieldGrpcService> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public MetaFieldGrpcService(
            ILogger<MetaFieldGrpcService> logger,
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public override async Task<PersistResponse> Persist(PersistRequest request, ServerCallContext context)
        {
            try
            {
                await _commandDispatcher.DispatchAsync(new PersistMetaFieldCommand
                {
                    Id = request.Id,
                    MetaField = request.MetaField,
                });

                return new PersistResponse
                {
                    StatusCode = PersistResponse.Types.StatusCode.Ok,
                };
            }
            catch (ArgumentException e)
            {
                _logger.LogError($"Failed persisting entity {request.Id}", e);
                return new PersistResponse
                {
                    StatusCode = PersistResponse.Types.StatusCode.Invalid,
                    Errors = { e.Message },
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed persisting entity {request.Id}", e);
            }

            return new PersistResponse
            {
                StatusCode = PersistResponse.Types.StatusCode.Unavailable,
            };
        }

        public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
        {
            try
            {
                await _commandDispatcher.DispatchAsync(new DeleteMetaFieldCommand
                {
                    Id = request.Id,
                });

                return new DeleteResponse
                {
                    StatusCode = DeleteResponse.Types.StatusCode.Ok,
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed deleting entity {request.Id}", e);
            }

            return new DeleteResponse
            {
                StatusCode = DeleteResponse.Types.StatusCode.Unavailable,
            };
        }

        public override async Task<GetByIdsResponse> GetByIds(GetByIdsRequest request, ServerCallContext context)
        {
            try
            {
                var entities = await _queryDispatcher.DispatchAsync<FetchMetaFieldsByIdsQuery, IList<Proto.Types.MetaField>>(new FetchMetaFieldsByIdsQuery
                {
                    Ids = request.Ids,
                });

                return new GetByIdsResponse
                {
                    MetaFields = { entities },
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed fetching by ids '{string.Join(",", request.Ids)}'", e);
            }

            return new GetByIdsResponse();
        }

        public override async Task<GetByParentIdsResponse> GetByParentIds(GetByParentIdsRequest request, ServerCallContext context)
        {
            try
            {
                var entities = await _queryDispatcher
                    .DispatchAsync<FetchMetaFieldsByParentIdsQuery, IDictionary<string, IList<Proto.Types.MetaField>>>(
                        new FetchMetaFieldsByParentIdsQuery
                        {
                            ParentIds = request.ParentIds,
                        });

                return new GetByParentIdsResponse
                {
                    MetaFields =
                    {
                        entities.ToDictionary(
                            k => k.Key,
                            v => new GetByParentIdsResponse.Types.MetaFieldList {MetaFields = {v.Value}})
                    },
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed fetching by parent ids '{string.Join(",", request.ParentIds)}'", e);
            }

            return new GetByParentIdsResponse();
        }

        public override async Task<GetBySearchResponse> GetBySearch(GetBySearchRequest request, ServerCallContext context)
        {
            try
            {
                var metaFields = await _queryDispatcher.DispatchAsync<FetchMetaFieldsBySearchQuery, IList<Proto.Types.MetaField>>(new FetchMetaFieldsBySearchQuery
                {
                    ParentId = request.ParentId,
                    Namespace = request.Namespace,
                    Name = request.Name
                });

                return new GetBySearchResponse
                {
                    Results = { metaFields },
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed fetching by search '{JsonSerializer.Serialize(request)}'", e);
            }

            return new GetBySearchResponse();
        }
    }
}
