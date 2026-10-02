# TechnologyMarquee

## Responsabilidad

Mostrar una cinta infinita de badges de tecnología con animación CSS pura.

## Inputs

| Parámetro | Tipo | Requerido | Descripción |
| --- | --- | --- | --- |
| `Badges` | `IReadOnlyList<TechnologyBadgeViewModel>` | Sí | Lista resuelta por el handler. |

## Outputs

- Dos copias de la lista para lograr un bucle sin saltos.
- `role="list"` en el contenedor; la copia duplicada es `aria-hidden="true"`.

## Dependencias

- `TechnologyBadge`.
- Keyframes `pf-marquee` en `Themes/theme.css`.

## Rendimiento y accesibilidad

- La animación se pausa al pasar el ratón y se desactiva por completo con
  `prefers-reduced-motion: reduce`.
- No hay JavaScript implicado: `transform` sobre el track.
