# Microservices Project Template
This template was born to be a start point to create Web Applications projects based on the use of microservices. The general architecture was designed and created following the main development techniques, the concepts of Domain Drive Design, observing the SOLID principles and the use of strongly recommended patterns such as Unit of Work and CQRS for example.

## Architecture schema

![Project_Architecture](/docs/SolutionArchitecture.png "Project_Architecture")

# WebApp, WebApi and EventBus projects composition.

> Style of creation to **WebApp MVC** projects  
> 1. **SolutionName**.*ProjectName*.
>       1. ProjectName.Common
>       2. ProjectName.Web
>       3. ProjectName.Web.Testing
>       4. ProjectName.Core
>       5. ProjectName.Data => [ ***For data management if necessary*** ]
>       6. ProjectName.Service [ ***For API service if necessary*** ]

> Style of creation to **WebApi Service** projects 
> 1. **SolutionName**.*ProjectName*.
>       1. ProjectName.Common
>       2. ProjectName.Api
>       3. ProjectName.Api.Testing
>       6. ProjectName.Core
>       7. ProjectName.Data => [ ***For data management if necessary*** ]
>       8. ProjectName.Service [ ***For API service if necessary*** ]

> Style of creation to **Bus Event**  projects
> 1. **SolutionName**.*ProjectName*.
>       1. ProjectName.Common
>       2. ProjectName.Handler
>       3. ProjectName.Handler.Testing
>       6. ProjectName.Core
>       7. ProjectName.Data => [ ***For data management if necessary*** ]
>       8. ProjectName.Service [ ***For API service if necessary*** ]
