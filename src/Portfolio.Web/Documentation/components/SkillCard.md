# SkillCard

## Responsabilidad

Mostrar un grupo de habilidades (Backend, Frontend, Mobile, Cloud,
Architecture) con su descripción, icono y lista de skills con nivel visual.

## Inputs

| Parámetro | Tipo | Requerido | Descripción |
| --- | --- | --- | --- |
| `Group` | `SkillGroupViewModel` | Sí | Grupo resuelto por `GetSkillGroupsQueryHandler`. |

`SkillGroupViewModel` contiene `Id`, `Title`, `Description`, `Icon`, `Accent` y
`Skills` (`SkillItemViewModel` con `LevelLabel`, `LevelPercentage`,
`LevelCssClass`).

## Outputs

- Tarjeta con encabezado y lista de skills.
- Barra de progreso con `role="img"` y `aria-label` ("Blazor: Expert").

## Dependencias

- `Icon`.
- Variable CSS `--skill-accent` para el tinte por disciplina.

## Casos de uso

- Página `/skills`, en una cuadrícula responsive.
- Reutilizable para una futura sección "stack" en la home o una página de
  detalle por disciplina.

## Accesibilidad

El nivel se comunica por texto (`Expert`, `Advanced`, `Intermediate`) además de
por color y barra; el color nunca es el único portador de información.
