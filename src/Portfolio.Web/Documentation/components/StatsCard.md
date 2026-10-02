# StatsCard

## Responsabilidad

Mostrar una métrica destacada con valor grande, etiqueta, descripción e icono.

## Inputs

| Parámetro | Tipo | Requerido | Descripción |
| --- | --- | --- | --- |
| `Stats` | `StatsCardViewModel` | Sí | `Value`, `Label`, `Description`, `Icon`, `Accent`. |

## Outputs

- `<div>` con borde de acento lateral y tipografía destacada.

## Dependencias

- `Icon`.
- `GetPortfolioStatisticsQueryHandler`, que garantiza que los números derivan
  de datos reales (opciones + repositorios) y no de texto escrito a mano.

## Casos de uso

- Fila de métricas del hero en la home.

## Escalabilidad

Acepta cualquier `Value` textual ("8+", "40+", "58"), por lo que puede mostrar
métricas no numéricas sin cambios.
