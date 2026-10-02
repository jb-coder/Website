# TechnologyBadge

## Responsabilidad

Pintar un único badge de tecnología, incluyendo icono, nombre y estilo visual
propio de su categoría. Todo el estilo viene resuelto por la estrategia
correspondiente.

## Inputs

| Parámetro | Tipo | Requerido | Descripción |
| --- | --- | --- | --- |
| `Badge` | `TechnologyBadgeViewModel` | Sí | `Name`, `CategoryLabel`, `CssClass`, `Icon`, `Accent`, `AriaLabel`. |

## Outputs

- `<span>` con clases de categoría (`pf-badge--backend`, etc.).
- Variable CSS `--badge-accent` para el tinte.

## Dependencias

- `Icon`.
- Familia `ITechnologyBadgeStrategy` (indirectamente, vía factory/handler).

## Casos de uso

- Tarjetas de proyecto, marquee de la home y cualquier futura vista que
  necesite mostrar tecnologías.

## Notas de diseño

La taxonomía se expresa también en la forma: pills para backend/mobile, esquinas
suaves para frontend/cloud y mayúsculas espaciadas para arquitectura. Así el
badge comunica la categoría incluso en escala de grises.
