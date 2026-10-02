# TimelineItem

## Responsabilidad

Renderizar una entrada de la línea temporal profesional: periodo, tipo
(experiencia, educación, certificación), rol, empresa, resumen, logros y
tecnologías.

## Inputs

| Parámetro | Tipo | Requerido | Descripción |
| --- | --- | --- | --- |
| `Entry` | `Experience` | Sí | Modelo de dominio ordenado por fecha descendente. |

## Outputs

- `<li>` con marcador e icono según `ExperienceType`.
- Etiquetas de tecnologías y logros con iconos de verificación.

## Dependencias

- `Icon`.
- `Experience.Period` (propiedad calculada, culture-invariant para que el HTML
  exportado sea determinista).

## Casos de uso

- Página `/about`, sección "Experience, education and certifications".
- Reutilizable para una futura página de CV detallado.

## Accesibilidad

- El marcador visual es decorativo (`aria-hidden`); la información está en el
  contenido textual.
- La lista externa es `<ol>` porque el orden cronológico es significativo.
