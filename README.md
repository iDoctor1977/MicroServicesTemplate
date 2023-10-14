# Microservices Project Template
This template was born to create Web Applications projects based on the use of microservices. The general architecture was designed and created following the main development techniques, the concepts of Domain Drive Design, observing the SOLID principles and the use of strongly recommended patterns such as Unit of Work and CQRS.

## Net Core microservices Architecture schema

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
