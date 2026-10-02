# Dependency Injection

## Qué es

La inyección de dependencias es un principio de inversión de control: un
componente declara **qué necesita** (abstracciones) y el contenedor decide
**quién se lo entrega** y con qué ciclo de vida. En .NET el contenedor vive en
`Microsoft.Extensions.DependencyInjection` y se configura al arrancar la
aplicación.

## Por qué se usa

- Elimina el acoplamiento entre consumidores e implementaciones.
- Hace explícitas las dependencias: el constructor es la documentación.
- Permite sustituir infraestructura (datos estáticos → API/CMS) sin tocar la UI.
- Es la base sobre la que se apoyan el resto de patrones del proyecto
  (Strategy, Factory, Builder, Mediator).

## Ventajas

| Ventaja | Impacto en este proyecto |
| --- | --- |
| Bajo acoplamiento | Las páginas solo conocen `IMediator` y las abstracciones. |
| Testabilidad | Cualquier servicio puede sustituirse por un doble de prueba. |
| Ciclos de vida claros | Repositorios singleton inmutables, handlers scoped por request. |
| Un único composition root | Toda la configuración está en `AddPortfolio`. |

## Implementación en este proyecto

El registro vive en un único punto:

```csharp
// Services/PortfolioServiceCollectionExtensions.cs
services.AddOptions<PortfolioOptions>()
    .Bind(configuration.GetSection(PortfolioOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

services.AddSingleton<IProjectRepository, ProjectRepository>();
services.AddSingleton<ITechnologyBadgeStrategy, BackendTechnologyBadgeStrategy>();
// ...
services.AddScoped<IMediator, PortfolioMediator>();
services.AddScoped<IRequestHandler<GetProjectsQuery, ...>, GetProjectsQueryHandler>();
```

`Program.cs` solo llama a `builder.Services.AddPortfolio(builder.Configuration)`.
Los componentes reciben sus dependencias con `@inject` y las páginas con
propiedades `[Inject]`.

## Diagrama

```mermaid
flowchart LR
    P[Home page] -->|@inject| M[IMediator]
    M --> H[GetProjectsQueryHandler]
    H --> R[IProjectRepository]
    H --> S[IProjectFilterStrategy]
    H --> F[IProjectCardFactory]
    R -.->|new| P[(ProjectRepository)]
    S -.->|new| B[AllProjectsFilterStrategy]
    F -.->|new| C[ProjectCardFactory]
```

La flecha punteada la decide el contenedor en tiempo de ejecución; el código
fuente solo conoce la flecha continua.
