# CoreServicesTemplate
Net Core microservices template 

# Base guideline to create Web App and Web Api Domain Driven projects.

> Style of creation for projects **WebApp MVC**  
> 1. **SolutionName**.*SolutionProject*.
>       1. Common
>       2. Web => [ ***Interfaccia grafica*** ]
>       3. Web.Testing
>       4. Core
>       5. Data => [ ***Per le WebApp di gestione dati*** ]
>       6. Services

> Style of creation for projects **WebApi MVC** 
> 1. **SolutionName**.*SolutionProject*.
>       1. Common
>       2. Api
>       3. Api.Testing
>       4. Web => [ ***Interfaccia grafica se necessaria*** ]
>       5. Web.Testing
>       6. Core
>       7. Data => [ ***Per le WebApi di gestione dati*** ]
>       8. Services

---

## Architecture components

> - ViewModel, ApiModel, AppModel, AggModel.
> - Aggregates (Domain objects definition)
> - Features
> - Pipeline Feature Substeps
> - Consolidators (default, custom)
> - Repositoryes (Repository pattern + CQRS pattern)
> - Depots (Unit of Works pattern)

---

## Project components.

> - Builder
> - Rebus o RabbitMq
> - Action Filter
> - Health Check
> - Mapper
