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

- Tarjeta con encabezado, lista de skills y pie con el recuento (`8 skills`).
- Barra de progreso con `role="img"` y `aria-label` ("Blazor: Expert").
- Pie anclado abajo (`margin-top: auto`) que cierra visualmente la tarjeta.

## Dependencias

- `Icon`.
- Variable CSS `--skill-accent` para el tinte por disciplina.

## Adaptación a grupos desiguales

Las categorías no tienen el mismo número de skills (de 4 a 8). Para que una
tarjeta con menos elementos no quede con huecos:

- La cuadrícula usa `align-items: start`, de modo que cada tarjeta mide lo que
  mide su contenido en lugar de estirarse a la altura de la fila.
- La tarjeta es un contenedor flex en columna y la lista usa `flex: 1` con
  `align-content: start`; el pie se ancla al fondo con `margin-top: auto`.
- El pie muestra el recuento de skills y una línea de acento, aportando un
  cierre consistente independientemente del número de filas.

## Casos de uso

- Página `/skills`, en una cuadrícula responsive.
- Reutilizable para una futura sección "stack" en la home o una página de
  detalle por disciplina.

## Accesibilidad

El nivel se comunica por texto (`Expert`, `Advanced`, `Intermediate`) además de
por color y barra; el color nunca es el único portador de información.
