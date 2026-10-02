# SectionHeader

## Responsabilidad

Renderizar la cabecera estándar de una sección: eyebrow opcional, título
obligatorio y subtítulo opcional, con alineación configurable.

## Inputs

| Parámetro | Tipo | Requerido | Descripción |
| --- | --- | --- | --- |
| `Section` | `PortfolioSection` | Sí | Construido con `PortfolioSectionBuilder`. |
| `Align` | `string` | No | `"start"` (defecto) o `"center"`. |

## Outputs

- `<header>` con `pf-section__header` y modificador de alineación.
- El `<h2>` da estructura semántica a la página.

## Dependencias

- `PortfolioSectionBuilder` en el consumidor (no dentro del componente).

## Casos de uso

- Todas las secciones de Home, About, Skills, Projects y Architecture.

## Por qué recibe un `PortfolioSection`

El Builder Pattern permite que el consumidor componga la sección con validación
(id y título obligatorios) y que el componente no negocie con cinco parámetros
sueltos. Ver `Documentation/builder.md`.
