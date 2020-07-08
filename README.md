# LightOps Commerce - Meta-field Service

Microservice for meta-fields.

Defines meta-fields.  
Uses CQRS to fetch entities from data-source without defining any.  
Provides gRPC services for integrations into other services.

![Nuget](https://img.shields.io/nuget/v/LightOps.Commerce.Services.MetaField)

| Branch | CI |
| --- | --- |
| master | ![Build Status](https://dev.azure.com/sorendev/LightOps%20Commerce/_apis/build/status/LightOps.Commerce.Services.MetaField?branchName=master) |
| develop | ![Build Status](https://dev.azure.com/sorendev/LightOps%20Commerce/_apis/build/status/LightOps.Commerce.Services.MetaField?branchName=develop) |

## gRPC services

Protobuf service definitions located at [SorenA/lightops-commerce-proto](https://github.com/SorenA/lightops-commerce-proto).

Meta Field v1 is implemented in `Domain.Services.V1.MetaFieldGrpcService`.

Health v1 is implemented in `Domain.Services.V1.HealthGrpcService`.

### Health-check

Health-checks conforms to the [GRPC Health Checking Protocol](https://github.com/grpc/grpc/blob/master/doc/health-checking.md)

Available services are as follows

```bash
service = '' - System as a whole
service = 'service.meta_field.v1.ProtoMetaFieldService' - MetaFieldPage v1
```

For embedding a gRPC client for use with Kubernetes, see [grpc-ecosystem/grpc-health-probe](https://github.com/grpc-ecosystem/grpc-health-probe)

## Samples

A sample application hosting the gRPC service with mock data is available in the `samples/Sample.MetaFieldService` project.

## Requirements

LightOps packages available on NuGet:

- `LightOps.DependencyInjection`
- `LightOps.CQRS`
- `LightOps.Mapping`

## Using the service component

Register during startup through the `AddMetaFieldService(options)` extension on `IDependencyInjectionRootComponent`.

```csharp
services.AddLightOpsDependencyInjection(root =>
{
    root
        .AddMapping()
        .AddCqrs()
        .AddMetaFieldService(service =>
        {
            // Configure service
            // ...
        });
});

services.AddGrpc();
```

Register gRPC services for integrations.

```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<MetaFieldGrpcService>();
    endpoints.MapGrpcService<HealthGrpcService>();

    // Map other endpoints...
});
```

### Configuration options

A component backend is required, defining the query handlers tied to a data-source, see **Query handlers** section bellow for more.

A custom backend, or one of the following standard backends can be used:

- InMemory

### Overridables

Using the `IMetaFieldServiceComponent` configuration, the following can be overridden:

```csharp
public interface IMetaFieldServiceComponent
{
    #region Services
    IMetaFieldServiceComponent OverrideHealthService<T>() where T : IHealthService;
    IMetaFieldServiceComponent OverrideMetaFieldService<T>() where T : IMetaFieldService;
    #endregion Services

    #region Mappers
    IMetaFieldServiceComponent OverrideProtoMetaFieldMapperV1<T>() where T : IMapper<IMetaField, Proto.Services.MetaField.V1.ProtoMetaField>;
    #endregion Mappers

    #region Query Handlers
    IMetaFieldServiceComponent OverrideCheckMetaFieldHealthQueryHandler<T>() where T : ICheckMetaFieldHealthQueryHandler;
    IMetaFieldServiceComponent OverrideIFetchMetaFieldByParentQueryHandler<T>() where T : IFetchMetaFieldByParentQueryHandler;
    IMetaFieldServiceComponent OverrideIFetchMetaFieldsByParentQueryHandler<T>() where T : IFetchMetaFieldsByParentQueryHandler;
    #endregion Query Handlers
}
```

`IMetaFieldService` is used by the gRPC services and query the data using the `IQueryDispatcher` from the `LightOps.CQRS` package.

The mappers are used for mapping the internal data structure to the versioned protobuf messages.

## Backend modules

### In-Memory

Register during startup through the `UseInMemoryBackend(root, options)` extension on `IMetaFieldServiceComponent`.

```csharp
root.AddMetaFieldService(service =>
{
    service.UseInMemoryBackend(root, backend =>
    {
        var metaFields = new List<IMetaFields>();
        // ...

        backend.UseMetaFields(metaFields);
    });

    // Configure service
    // ...
});
```
