# ContactCard

## Responsabilidad

Mostrar un canal de contacto como tarjeta enlazada: icono, etiqueta y valor.

## Inputs

| Parámetro | Tipo | Requerido | Descripción |
| --- | --- | --- | --- |
| `Label` | `string` | Sí | Nombre del canal ("Email", "LinkedIn"). |
| `Value` | `string` | Sí | Texto visible (dirección, descripción). |
| `Href` | `string` | Sí | Destino del enlace. |
| `Icon` | `string` | Sí | Icono del sprite. |
| `Accent` | `string` | No | Color de acento (hex). |
| `External` | `bool` | No | `true` por defecto; añade `target`/`rel` seguros. |

## Outputs

- `<a>` estilizado como tarjeta con flecha de acción.
- Texto oculto "(opens in a new tab)" cuando `External` es verdadero.

## Dependencias

- `Icon`.

## Casos de uso

- Página `/contact` para email, LinkedIn, GitHub y ubicación.
