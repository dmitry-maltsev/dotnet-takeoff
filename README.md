# Dotnet Takeoff

Solution template for bootstrapping modern .NET applications

## Building Blocks
- Entity
- ValueObject
- Enumeration

## Application
- Project structure
- Command and Queries
- Dependency injection
- Automapping / projections
- Configuration
  - Azure App Configuration
- Secrets
  - User secrets
  - Azure Key Vault
- Docker support
- Docker compose for local development
- Input validation
  - FluentValidation
  - Validation filter
- Global error handling
  - Domain exception
  - ProblemDetails
  - Exception logging
  - Trace ID
- Authentication
  - Microsoft Identity
  - user-jwts
- Authorization
- Open API / Swagger
- Health checks
- CORS
- Guard clauses
- Output cache
- Distributed cache
- Resiliency policies
- Response caching
- Response compression
- Security headers
- HSTS / HTTPS redirection / Forward headers
- API versioning

## Tests
- Unit
- Functional
- Integration

## Data Access
- Database connection
- Entity Framework Core / Dapper
- No tracking
- Split queries
- Transactions
- Concurrency handling
- Data schema migrations
  - DbUp
  - Liquibase
- Filtering / pagination / sorting
- Database logging

## Observability
- Application logging
  - Serilog
  - Serilog ASP.NET Core
  - Request start time
  - Seq
- Serilog enrichers
  - Event Type
  - Activity ID
- Bootstrap logging
- Audit logging
- Metrics
- Tracing
- OpenTelemetry
- APM integration (Application Insights)
- Application version

## Code Quality
- .editorconfig
- Static code analysis
- Directory.Build.props

## Services and Integrations
- Worker
- Scheduled jobs
- Dapr
- YARP
- gRPC
- Message bus (RabbitMQ / Kafka / Azure Service Bus)

## Links
- [.NET Microservices: Architecture for Containerized .NET Applications](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/)
- [Architecting Cloud Native .NET Applications for Azure](https://docs.microsoft.com/en-us/dotnet/architecture/cloud-native/)
- [Dapr for .NET Developers](https://docs.microsoft.com/en-us/dotnet/architecture/dapr-for-net-developers/)
- [Dotnet Podcasts](https://github.com/microsoft/dotnet-podcasts)
- [David Fowler's TodoApi](https://github.com/davidfowl/TodoApi)
