# HeroSection

## Responsabilidad

Presentar al profesional en los primeros segundos: disponibilidad, nombre, rol,
propuesta de valor, CTAs, áreas de foco, tarjeta de código, métricas y cinta de
tecnologías.

## Inputs

| Parámetro | Tipo | Requerido | Descripción |
| --- | --- | --- | --- |
| `Profile` | `LocalizedProfile` | Sí | Datos del perfil validados y ya traducidos al idioma de la petición. |
| `Stats` | `IReadOnlyList<StatsCardViewModel>` | Sí | Métricas calculadas por el handler. |
| `Technologies` | `IReadOnlyList<TechnologyBadgeViewModel>` | Sí | Stack destacado (puede ir vacío). |

## Outputs

- `<section id="hero">` accesible con `aria-labelledby`.
- CTAs hacia `projects` y `contact`.
- Tarjeta de código con datos reales de configuración.
- Enlaces sociales (GitHub, LinkedIn, email).

## Dependencias

- `StatsCard`, `TechnologyMarquee`, `Icon`.
- `PortfolioOptions` (Options Pattern) resuelto por `LocalizedProfile`.
- `ITranslator` para los textos fijos y `ILanguageContext` para los enlaces.
- Sin servicios: los datos llegan resueltos desde `Home.razor.cs`.

## Casos de uso

- Home. Es el único consumidor; si otra página necesitara el hero, se
  parametrizaría en lugar de duplicarse (DRY).
