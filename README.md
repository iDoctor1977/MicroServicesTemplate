# Microservices Project Template
Questo template nasce per realizzare progetti di Applicazioni Web basate sull'utilizzo di microservizi. L'architettura generale è stata progettata e realizzata seguendo le principali tecniche di sviluppo, i concetti di Domain Drive Design, osservando i principi SOLID e l'utilizzo di pattern fortemente consigliati quali Unit of Work e CQRS. 

> Net Core microservices Architecture schema

![Project_Architecture](/docs/SolutionArchitecture.png "Project_Architecture")

# Basic architecture to create Web App and Web Api Domain Driven projects.

> Style of creation for projects **WebApp MVC**  
> 1. **SolutionName**.*ProjectName*.
>       1. ProjectName.Common
>       2. ProjectName.Web => [ ***Graphic user interface*** ]
>       3. ProjectName.Web.Testing
>       4. ProjectName.Core
>       5. ProjectName.Data => [ ***For data management*** ]
>       6. ProjectName.Services

> Style of creation for projects **WebApi MVC** 
> 1. **SolutionName**.*ProjectName*.
>       1. ProjectName.Common
>       2. ProjectName.Api
>       3. ProjectName.Api.Testing
>       6. ProjectName.Core
>       7. ProjectName.Data => [ ***For data management*** ]
>       8. ProjectName.Services

---

## Architecture components

> - ViewModel, AppModel, Model, ApiDto. BusDto, Entity (Model and DTO)
> - Aggregates and Childs (Domain objects definition)
> - Features
> - Mappers (default, custom)
> - Repositories (Repository pattern + CQRS pattern)
> - Depots (Unit of Works pattern)

---

## Accessories project components.

> - Builder
> - Event Bus
> - Action Filter
> - Health Check
> - Mapper
