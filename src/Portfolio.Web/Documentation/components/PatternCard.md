# PatternCard

## Responsabilidad

Presentar un patrón de diseño aplicado: intención, implementación concreta en
este repositorio, beneficio y enlace a su documentación.

## Inputs

| Parámetro | Tipo | Requerido | Descripción |
| --- | --- | --- | --- |
| `Pattern` | `ArchitecturePattern` | Sí | Modelo del catálogo de patrones. |
| `DocumentationUrl` | `string` | Sí | URL absoluta del documento Markdown en GitHub. |

## Outputs

- `<article>` con `<dl>` semántico (Intent / In this project / Why it pays off).
- Enlace externo con indicación accesible de apertura en pestaña nueva.

## Dependencias

- `Icon`.
- `PortfolioOptions.BuildDocumentationUrl` para componer la URL.

## Casos de uso

- Página `/architecture`, sección "Design patterns applied in production code".

## Nota

La URL se calcula en la página y se pasa como parámetro para que el componente
no dependa de la configuración (inversión de dependencias también en UI).
