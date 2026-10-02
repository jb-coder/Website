# Catálogo de componentes

Todos los componentes son Razor Components reutilizables, sin estado propio y
sin JavaScript. Reciben view models ya resueltos por la capa de aplicación.

| Componente | Carpeta | Propósito |
| --- | --- | --- |
| [HeroSection](HeroSection.md) | `Components/Sections` | Cabecera principal de la home |
| [SectionHeader](SectionHeader.md) | `Components/Shared` | Cabecera estándar de sección |
| [ProjectCard](ProjectCard.md) | `Components/Shared` | Tarjeta de proyecto |
| [SkillCard](SkillCard.md) | `Components/Shared` | Grupo de skills con niveles |
| [TimelineItem](TimelineItem.md) | `Components/Shared` | Entrada de la línea temporal |
| [TechnologyBadge](TechnologyBadge.md) | `Components/Shared` | Badge de tecnología |
| [TechnologyMarquee](TechnologyMarquee.md) | `Components/Shared` | Cinta infinita de tecnologías |
| [StatsCard](StatsCard.md) | `Components/Shared` | Métrica destacada |
| [PatternCard](PatternCard.md) | `Components/Shared` | Patrón documentado |
| [ContactCard](ContactCard.md) | `Components/Shared` | Canal de contacto |
| [Icon](Icon.md) | `Components/Shared` | Icono SVG desde sprite |

## Convenciones

- Parámetros `[Parameter, EditorRequired]` para dependencias obligatorias.
- Sin lógica de negocio: si hay que decidir algo, vive en un handler.
- Accesibilidad: `aria-hidden` en decoración, `aria-label` en iconografía
  interactiva, textos ocultos para enlaces que abren pestaña nueva.
- Clases CSS con prefijo `pf-` definidas en `wwwroot/Themes`.
