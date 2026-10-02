# ProjectCard

## Responsabilidad

Renderizar un proyecto ya proyectado a `ProjectCardViewModel`: cabecera visual,
metadatos, descripción, hitos, badges de tecnología, estado y enlaces. Es
puramente declarativo; no conoce el modelo `Project` ni las estrategias.

## Inputs

| Parámetro | Tipo | Requerido | Descripción |
| --- | --- | --- | --- |
| `Project` | `ProjectCardViewModel` | Sí | View model construido por `ProjectCardFactory`. |

El view model incluye: `Id`, `Title`, `Summary`, `Description`,
`CategoryLabel`, `StatusLabel`, `StatusCssClass`, `Year`, `Accent`, `Icon`,
`IsFeatured`, `Highlights`, `Badges`, `RepositoryUrl`, `LiveUrl`.

## Outputs

- HTML semántico `<article>` con `<footer>` interno.
- Enlaces externos solo si `RepositoryUrl`/`LiveUrl` tienen valor.
- Sin eventos ni callbacks: el componente no es interactivo.

## Dependencias

- `TechnologyBadge` para cada badge.
- `Icon` para iconografía.
- Variables CSS `--project-accent` para el tinte por proyecto.

## Casos de uso

- Home: proyectos destacados (`GetFeaturedProjectsQuery`).
- `/projects`: catálogo completo o filtrado por categoría.
- Futuras vistas: búsqueda, favoritos o página de detalle.

## Accesibilidad

- Toda la tarjeta es contenido textual; el icono de cabecera es decorativo
  (`aria-hidden`).
- Los enlaces externos incluyen "(opens in a new tab)" para lectores de pantalla.
- El estado no depende solo del color: incluye texto (`In production`,
  `Delivered`, `In progress`).
