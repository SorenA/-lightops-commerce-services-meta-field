using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Grpc.Core;
using LightOps.Commerce.Proto.Services;
using LightOps.Commerce.Proto.Types;
using LightOps.Commerce.Services.MetaField.Api.Models;
using LightOps.Commerce.Services.MetaField.Api.Services;
using LightOps.Mapping.Api.Services;
using Microsoft.Extensions.Logging;

namespace LightOps.Commerce.Services.MetaField.Domain.Services.Grpc
{
    public class MetaFieldGrpcService : MetaFieldProtoService.MetaFieldProtoServiceBase
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

        public override async Task<GetMetaFieldsByIdsProtoResponse> GetMetaFieldsByIds(GetMetaFieldsByIdsProtoRequest request, ServerCallContext context)
        {
            var entities = await _metaFieldService.GetByIdAsync(request.Ids);
            var protoEntities = _mappingService.Map<IMetaField, MetaFieldProto>(entities);

            var result = new GetMetaFieldsByIdsProtoResponse();
            result.MetaFields.AddRange(protoEntities);

            return result;
        }

        public override async Task<GetMetaFieldsByParentIdsProtoResponse> GetMetaFieldsByParentIds(GetMetaFieldsByParentIdsProtoRequest request, ServerCallContext context)
        {
            var entityMap = await _metaFieldService.GetByParentIdsAsync(request.ParentIds);

            var result = new GetMetaFieldsByParentIdsProtoResponse();

            foreach (var keyPair in entityMap)
            {
                // Map meta-fields for parent id
                var protoEntities = _mappingService.Map<IMetaField, MetaFieldProto>(keyPair.Value);

                var metaFieldList = new GetMetaFieldsByParentIdsProtoResponse.Types.MetaFieldList();
                metaFieldList.MetaFields.AddRange(protoEntities);

                // Add to map
                result.MetaFields.Add(keyPair.Key, metaFieldList);
            }

            return result;
        }

        public override async Task<GetMetaFieldsBySearchProtoResponse> GetMetaFieldsBySearch(GetMetaFieldsBySearchProtoRequest request, ServerCallContext context)
        {
            var searchResult = await _metaFieldService.GetBySearchAsync(
                request.ParentId,
                request.Namespace,
                request.Name);

            var protoEntities = _mappingService
                .Map<IMetaField, MetaFieldProto>(searchResult)
                .ToList();

            var result = new GetMetaFieldsBySearchProtoResponse();
            result.Results.AddRange(protoEntities);

            return result;
        }
    }
}
