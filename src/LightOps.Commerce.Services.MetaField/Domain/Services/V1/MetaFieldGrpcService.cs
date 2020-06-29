using System.Threading.Tasks;
using Grpc.Core;
using LightOps.Commerce.Proto.Services.MetaField.V1;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Commerce.Services.MetaField.Api.Services;
using LightOps.Mapping.Api.Services;
using Microsoft.Extensions.Logging;

namespace LightOps.Commerce.Services.MetaField.Domain.Services.V1
{
    public class MetaFieldGrpcService : ProtoMetaFieldService.ProtoMetaFieldServiceBase
    {
        private readonly ILogger<MetaFieldGrpcService> _logger;
        private readonly IMetaFieldService _metaFieldService;
        private readonly IMappingService _mappingService;

        public MetaFieldGrpcService(
            ILogger<MetaFieldGrpcService> logger,
            IMetaFieldService metaFieldService,
            IMappingService mappingService)
        {
            _logger = logger;
            _metaFieldService = metaFieldService;
            _mappingService = mappingService;
        }

        public override async Task<ProtoGetMetaFieldByParentResponse> GetMetaFieldByParent(ProtoGetMetaFieldByParentRequest request, ServerCallContext context)
        {
            var entity = await _metaFieldService.GetByParentAsync(request.ParentEntityType, request.ParentEntityId, request.Name);
            var protoEntity = _mappingService.Map<IMetaField, ProtoMetaField>(entity);

            var result = new ProtoGetMetaFieldByParentResponse
            {
                MetaField = protoEntity
            };

            return result;
        }

        public override async Task<ProtoGetMetaFieldsByParentResponse> GetMetaFieldsByParent(ProtoGetMetaFieldsByParentRequest request, ServerCallContext context)
        {
            var entities = await _metaFieldService.GetByParentAsync(request.ParentEntityType, request.ParentEntityId);
            var protoEntities = _mappingService.Map<IMetaField, ProtoMetaField>(entities);

            var result = new ProtoGetMetaFieldsByParentResponse();
            result.MetaFields.AddRange(protoEntities);

            return result;
        }
    }
}
