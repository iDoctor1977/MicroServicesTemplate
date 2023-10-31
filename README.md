# .Net Core Microservices Template
This template was born to be a start point to create Web Applications based on the use of microservices. The general architecture was designed and created following the main development techniques, the concepts of Domain Drive Design, observing the SOLID principles and the use of strongly recommended patterns such as, for example, Unit of Work and CQRS.

## Architecture schema

![Project_Architecture](/docs/SolutionArchitecture.png "Project_Architecture")

# WebApp, WebApi and EventBus projects composition.

> Style of creation to **WebApp MVC** projects  
> 1. **SolutionName**.*ProjectName*
>       1. *SolutionName*.ProjectName.Common
>       2. *SolutionName*.ProjectName.Web
>       3. *SolutionName*.ProjectName.Web.Testing
>       4. *SolutionName*.ProjectName.Core
>       5. *SolutionName*.ProjectName.Data => [ ***For data management if necessary*** ]
>       6. *SolutionName*.ProjectName.Service => [ ***For API service if necessary*** ]

> Style of creation to **WebApi Service** projects 
> 1. **SolutionName**.*ProjectName*
>       1. *SolutionName*.ProjectName.Common
>       2. *SolutionName*.ProjectName.Api
>       3. *SolutionName*.ProjectName.Api.Testing
>       6. *SolutionName*.ProjectName.Core
>       7. *SolutionName*.ProjectName.Data => [ ***For data management if necessary*** ]
>       8. *SolutionName*.ProjectName.Service => [ ***For API service if necessary*** ]

> Style of creation to **Bus Event**  projects
> 1. **SolutionName**.*ProjectName*
>       1. *SolutionName*.ProjectName.Common
>       2. *SolutionName*.ProjectName.Handler
>       3. *SolutionName*.ProjectName.Handler.Testing
>       6. *SolutionName*.ProjectName.Core
>       7. *SolutionName*.ProjectName.Data => [ ***For data management if necessary*** ]
>       8. *SolutionName*.ProjectName.Service => [ ***For API service if necessary*** ]

> Style of creation to **Shared**  projects
> 1. **SolutionName**.Shared
>       1. *SolutionName*.Shared .Core
