# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.3.1] - 2020-12-03

### Changed

- In-memory backend meta-field provider made overridable on startup
- In-memory query handlers now support meta-field collection being null

## [0.3.0] - 2020-08-11

### Changed

- **Breaking** - Updated refactored service definition
- **Breaking** - Removed deprecated queries, query handlers and service methods
- **Breaking** - Extending search query, adding cursor-based pagination
- **Breaking** - Changed health-check service name

## [0.2.0] - 2020-07-04

### Changed

- Service lifespans changed to transient from scoped
- Query handler lifespan changed to transient from scoped
- Mapper lifespan changed to transient from scoped

## [0.1.0] - 2020-06-29

### Added

- CHANGELOG file
- README file describing project
- Azure Pipelines based CI/CD setup
- Meta Field v1 gRPC server implementation and mappers
- Health v1 gRPC server implementation and mappers
- Meta Field models
- Sample application with mock data
- Queries and query handlers for fetching data and running health-checks
- Meta Field service using CQRS for data retrival
- Health service using CQRS for status checks
- In-memory backend providing default query handlers

[unreleased]: https://github.com/SorenA/lightops-commerce-services-meta-field/compare/0.3.1...develop
[0.3.1]: https://github.com/SorenA/lightops-commerce-services-meta-field/tree/0.3.1
[0.3.0]: https://github.com/SorenA/lightops-commerce-services-meta-field/tree/0.3.0
[0.2.0]: https://github.com/SorenA/lightops-commerce-services-meta-field/tree/0.2.0
[0.1.0]: https://github.com/SorenA/lightops-commerce-services-meta-field/tree/0.1.0
