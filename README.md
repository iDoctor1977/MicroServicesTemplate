# Microservices Project Template
This template was born to create Web Applications projects based on the use of microservices. The general architecture was designed and created following the main development techniques, the concepts of Domain Drive Design, observing the SOLID principles and the use of strongly recommended patterns such as Unit of Work and CQRS.

## Project architecture schema

![Project_Architecture](/docs/SolutionArchitecture.png "Project_Architecture")

# Model vs DTO
Differenze tra Dto, Model e Oggetti di Dominio:

Model: 
tilizzato per fornire dati da un livello di applicazione ad un altro, ad es. Controller MVC <--> Vista MVC.
non ha una logica aziendale.
può essere soggetto a qualche logica di valdazione ecc.

Dto: 
utilizzato per trasferire i dati oltre i limiti dell'applicazione, ad es. tra web server e browser web.
solo proprietà/campi di tipo semplice ( stringhe, numeri, datetimes, booleani).
Oggetto di dominio:
rappresenta un oggetto del mondo reale.
a una logica aziendale.
non consente lo stato oggetto non valido e ha metodi per modificare correttamente lo stato dell'oggetto.

# Basic architecture to create Web App and Web Api Domain Driven projects.

> Style of creation for projects **WebApp MVC**  
> 1. **SolutionName**.*ProjectName*.
>       1. ProjectName.Common
>       2. ProjectName.Web
>       3. ProjectName.Web.Testing
>       4. ProjectName.Core
>       5. ProjectName.Data => [ ***For data management if necessary*** ]
>       6. ProjectName.Service [ ***For API service if necessary*** ]

> Style of creation for projects **WebApi Service** 
> 1. **SolutionName**.*ProjectName*.
>       1. ProjectName.Common
>       2. ProjectName.Api
>       3. ProjectName.Api.Testing
>       6. ProjectName.Core
>       7. ProjectName.Data => [ ***For data management if necessary*** ]
>       8. ProjectName.Service [ ***For API service if necessary*** ]

> Style of creation for projects **Bus Event** 
> 1. **SolutionName**.*ProjectName*.
>       1. ProjectName.Common
>       2. ProjectName.Handler
>       3. ProjectName.Handler.Testing
>       6. ProjectName.Core
>       7. ProjectName.Data => [ ***For data management if necessary*** ]
>       8. ProjectName.Service [ ***For API service if necessary*** ]
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
