# Icon

## Responsabilidad

Renderizar un icono vectorial desde el sprite SVG compartido
(`wwwroot/Assets/icons.svg`) usando `<use>`.

## Inputs

| Parámetro | Tipo | Requerido | Descripción |
| --- | --- | --- | --- |
| `Name` | `string` | Sí | Id del símbolo (`server`, `cloud`, `github`...). |
| `Size` | `int` | No | Tamaño en píxeles (20 por defecto). |
| `StrokeWidth` | `double` | No | Grosor de trazo (2 por defecto). |
| `CssClass` | `string?` | No | Clases adicionales. |

## Outputs

- `<svg>` con `stroke="currentColor"`, `aria-hidden="true"` y `focusable="false"`.

## Dependencias

- Sprite `Assets/icons.svg`.
- Color heredado del contenedor vía `currentColor`.

## Casos de uso

- Toda la iconografía del sitio: navegación, tarjetas, timeline, contacto.

## Ventajas

- **Una sola petición** para todos los iconos (sprite cacheable).
- El color se controla con CSS (`color`), nunca con atributos incrustados.
- Al ser decorativo por defecto, los enlaces iconográficos añaden su propio
  `aria-label`, evitando dobles lecturas.
