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

Meta Field is implemented in `Domain.GrpcServices.MetaFieldGrpcService`.

Health is implemented in `Domain.GrpcServices.HealthGrpcService`.

### Health-check

Health-checks conforms to the [GRPC Health Checking Protocol](https://github.com/grpc/grpc/blob/master/doc/health-checking.md)

Available services are as follows

```bash
service = '' - System as a whole
service = 'lightops.service.MetaFieldService' - MetaField
```

For embedding a gRPC client for use with Kubernetes, see [grpc-ecosystem/grpc-health-probe](https://github.com/grpc-ecosystem/grpc-health-probe)

## Samples

A sample application hosting the gRPC service with mock data is available in the `samples/Sample.MetaFieldService` project.

## Requirements

LightOps packages available on NuGet:

- `LightOps.DependencyInjection`
- `LightOps.CQRS`

## Using the service component

Register during startup through the `AddMetaFieldService(options)` extension on `IDependencyInjectionRootComponent`.

```csharp
services.AddLightOpsDependencyInjection(root =>
{
    root
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

The gRPC services use `ICommandDispatcher` & `IQueryDispatcher` from the `LightOps.CQRS` package to dispatch commands and queries, see configuration bellow.

### Configuration options

A component backend is required, implementing the command & query handlers tied to a data-source, see configuration overridables bellow.

A custom backend, or one of the following standard backends can be used:

- InMemory

### Overridables

Using the `IMetaFieldServiceComponent` configuration, the following can be overridden:

```csharp
public interface IMetaFieldServiceComponent
{
    #region Query Handlers
    IMetaFieldServiceComponent OverrideCheckMetaFieldServiceHealthQueryHandler<T>() where T : ICheckMetaFieldServiceHealthQueryHandler;
    IMetaFieldServiceComponent OverrideFetchMetaFieldsByIdsQueryHandler<T>() where T : IFetchMetaFieldsByIdsQueryHandler;
    IMetaFieldServiceComponent OverrideFetchMetaFieldsByParentIdsQueryHandler<T>() where T : IFetchMetaFieldsByParentIdsQueryHandler;
    IMetaFieldServiceComponent OverrideFetchMetaFieldsBySearchQueryHandler<T>() where T : IFetchMetaFieldsBySearchQueryHandler;
    #endregion Query Handlers

    #region Command Handlers
    IMetaFieldServiceComponent OverridePersistMetaFieldCommandHandler<T>() where T : IPersistMetaFieldCommandHandler;
    IMetaFieldServiceComponent OverrideDeleteMetaFieldCommandHandler<T>() where T : IDeleteMetaFieldCommandHandler;
    #endregion Command Handlers
}
```

## Backend modules

### In-Memory

Register during startup through the `UseInMemoryBackend(root, options)` extension on `IMetaFieldServiceComponent`.

```csharp
root.AddMetaFieldService(service =>
{
    service.UseInMemoryBackend(root, backend =>
    {
        var metaFields = new List<MetaField>();
        // ...

        backend.UseMetaFields(metaFields);
    });

    // Configure service
    // ...
});
```
