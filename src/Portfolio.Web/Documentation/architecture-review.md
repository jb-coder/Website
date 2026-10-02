# Revisión arquitectónica — Portfolio v1.0.0

> Fecha: 2026-10-02 · Rama: `release/v1.0.0` · Alcance: solución completa
> (`src/Portfolio.Web` + `tools/Portfolio.StaticExporter` + workflow CI/CD).

## 1. Resumen ejecutivo

La solución cumple los objetivos planteados: sitio estático compatible con
GitHub Pages, arquitectura limpia proporcionada al tamaño del problema, siete
patrones de diseño aplicados con documentación individual, componentes
reutilizables y calidad verificada por el build.

**Veredicto: aprobada con reservas menores.** Las reservas no bloquean la
publicación y están listadas como recomendaciones en la sección 12.

## 2. Evidencia de verificación

| Comprobación | Comando / método | Resultado |
| --- | --- | --- |
| Compilación Release | `dotnet build -c Release` | 0 errores, 0 advertencias |
| Arranque de la app | binario publicado + `ASPNETCORE_URLS` | OK, `OptionsValidationException` si falta configuración |
| Rutas principales | `GET /`, `/about`, `/skills`, `/projects`, `/architecture`, `/contact` | 200 en todas |
| Filtro por query | `GET /projects?category=Api` | 200, contenido filtrado |
| Ruta inexistente | `GET /no-existe` | 404 con página propia |
| `sitemap.xml` / `robots.txt` | `GET` directo | 200, XML/texto válidos |
| Sub-path GitHub Pages | `Portfolio__BasePath=/website/` | `<base href>` correcto, assets 200 |
| Export estático | `scripts/export-static.ps1` | 7 HTML + assets, `dist/` completo |
| Metadatos SEO | canonical en `dist/index.html` | `https://javierbaena.github.io/website` |
| Accesibilidad estructural | análisis de `aria-labelledby` en las 7 páginas | 0 referencias rotas |
| JavaScript en el sitio | búsqueda de `_framework` en `dist/` | Ausente por diseño |

## 3. Principios SOLID

| Principio | Evidencia | Valoración |
| --- | --- | --- |
| **S**RP | Estrategias que solo pintan badges; factoría que solo proyecta; repositorios que solo sirven datos; una página = una ruta. | Cumple |
| **O**CP | Añadir una categoría = nueva `ITechnologyBadgeStrategy` + registro; no se modifica ninguna clase existente. | Cumple |
| **L**SP | Todas las estrategias respetan el contrato de su interfaz; las bases reutilizan la lógica común. | Cumple |
| **I**SP | Interfaces pequeñas: `ISkillRepository`, `IExperienceRepository`, `ITechnologyBadgeStrategy`… ninguna interfaz "dios". | Cumple |
| **D**IP | Páginas → `IMediator`; handlers → abstracciones; UI → view models. Ninguna capa conoce implementaciones concretas. | Cumple |

**Observación**: `IProjectRepository.GetByIdAsync` no se consume todavía. Se
considera deuda consciente aceptable en una abstracción de datos estable, pero
si en v2 no aparece un consumidor, debería eliminarse (YAGNI estricto).

## 4. Patrones implementados

| Patrón | Implementación | Documento | Estado |
| --- | --- | --- | --- |
| Dependency Injection | `AddPortfolio` como composition root | ✅ | Correcto |
| Options | `PortfolioOptions` + `ValidateOnStart` | ✅ | Verificado en ejecución real |
| Repository | 5 repositorios in-memory tras interfaces | ✅ | Correcto |
| Strategy | Badges (5) + filtros (5) por categoría | ✅ | Correcto |
| Factory | `ProjectCardFactory` delega en estrategias | ✅ | Correcto |
| Builder | `PortfolioSectionBuilder` con validación en `Build()` | ✅ | Correcto |
| Mediator (light) | `PortfolioMediator` con requests y notifications | ✅ | Correcto, sin dependencias |
| Static Rendering | `Portfolio.StaticExporter` + workflow | ✅ | Verificado end-to-end |

Colaboración entre patrones verificada: la factoría consume estrategias, el
mediador consume handlers, los handlers consumen repositorios y estrategias.

## 5. Reutilización de componentes

| Componente | Consumidores | Reutilización |
| --- | --- | --- |
| `Icon` | Todos los componentes y páginas | Muy alta |
| `TechnologyBadge` | `ProjectCard`, `TechnologyMarquee` | Alta |
| `SectionHeader` | Home, About, Skills, Projects, Architecture, Contact | Muy alta |
| `ProjectCard` | Home (2), Projects (2) | Alta |
| `SkillCard` | Skills (5 grupos) | Media |
| `TimelineItem` | About (5 entradas) | Media |
| `ContactCard` | Contact (4) | Media |
| `StatsCard` | Home (4) | Baja |
| `PatternCard` | Architecture (8) | Media |
| `TechnologyMarquee` | Home | Baja |
| `HeroSection` | Home | Baja (por definición) |

No se detectó lógica duplicada en marcado: los cálculos se centralizan en
handlers y factorías; los componentes solo pintan.

## 6. Accesibilidad (objetivo AA)

| Criterio | Evidencia | Estado |
| --- | --- | --- |
| Landmarks | `header`, `nav[aria-label]`, `main`, `footer` | ✅ |
| Skip link | `.pf-skip-link` hacia `#main` con foco visible | ✅ |
| Jerarquía de encabezados | Un `h1` por página; `h2` de sección; `h3` en tarjetas | ✅ |
| `aria-labelledby` | 0 referencias rotas tras la corrección de la revisión | ✅ (corregido) |
| Enlaces externos | `rel="noopener noreferrer"` + texto "(opens in a new tab)" | ✅ |
| Iconografía | Decorativa con `aria-hidden`; informativa con `aria-label` | ✅ |
| Color | El estado nunca depende solo del color (texto + punto) | ✅ |
| Contraste | Texto principal `#E6EDF3` y azul `#58A6FF` sobre `#0D1117` > 4.5:1 | ✅ |
| Movimiento | `prefers-reduced-motion` desactiva animaciones | ✅ |
| Formularios | No existen; el contacto usa enlaces | N/A |

**Hallazgo corregido durante la revisión**: varias secciones declaraban
`aria-labelledby` hacia ids inexistentes. `SectionHeader` ahora genera
`id="{Section.Id}-title"` y las páginas referencian el id correcto. Verificado
por script en las 7 páginas exportadas: 0 referencias rotas.

**Recomendación**: ejecutar una auditoría automática (axe-core/Lighthouse) en CI
para cubrir lo que el análisis estático no ve (orden de tabulación real, etc.).

## 7. Rendimiento

| Aspecto | Medición / decisión |
| --- | --- |
| JavaScript en el sitio | 0 KB: `blazor.web.js` no se referencia ni se exporta. |
| HTML home | ~47 KB sin comprimir, ~6 KB gzip estimado. |
| CSS | 4 archivos temáticos, sin frameworks externos. |
| Iconos | Un sprite SVG cacheable para toda la iconografía. |
| Fuentes | Solo fuentes del sistema: sin peticiones externas. |
| Artefacto `dist/` | 18 archivos; sin `_framework` ni `.br`/`.gz` duplicados. |
| Animaciones | CSS puro (`animation-timeline`, `transform`), sin bloqueo del hilo. |
| Dependencia externa | Mermaid vía CDN solo en `/architecture`; el diagrama por capas funciona sin JS. |

Con este perfil, Lighthouse 95+ es alcanzable, pero **no se ejecutó Lighthouse**
en esta revisión: se recomienda añadirlo como paso de CI (ver sección 12).

## 8. Escalabilidad

| Extensión futura | Punto de entrada preparado | Impacto estimado |
| --- | --- | --- |
| Blog | Nueva feature + repositorio + ruta | Bajo |
| API/CMS | Implementar `IProjectRepository` contra HTTP | Bajo (no toca UI) |
| Multiidioma | `IStringLocalizer` + recursos; el contenido está en componentes | Medio |
| Analytics | `PageVisitedNotification` ya se publica | Bajo |
| Interactividad puntual | `@rendermode InteractiveServer` por componente | Medio (cambio de hosting) |
| Búsqueda estática | Nuevo query + índice generado en export | Bajo |

## 9. Calidad de código

- `Nullable` e `ImplicitUsings` habilitados a nivel de solución.
- `TreatWarningsAsErrors=true`: la build no admite advertencias.
- `EnforceCodeStyleInBuild` + `.editorconfig` (IDE0005 incluida).
- Namespaces file-scoped, `record`s inmutables, colecciones de solo lectura.
- `Directory.Build.props` centraliza la política; ningún proyecto la repite.
- Documentación XML en la capa de servicios y documentación Markdown por patrón
  y componente.
- Sin comentarios redundantes en el código de producción.

**Observación menor**: `ProjectRepository` concentra los proyectos de seed en un
solo archivo; si el catálogo crece, conviene extraer los datos a un archivo de
seed o a JSON embebido, manteniendo el repositorio como única puerta.

## 10. Responsividad

| Breakpoint | Comportamiento verificado por CSS |
| --- | --- |
| ≥ 980 px | Hero a dos columnas, stats en 4 columnas. |
| ≤ 980 px | Columnas apiladas, stats en 2. |
| ≤ 960 px | Navegación en fila desplazable, header en dos líneas. |
| ≤ 900 px | About apilado, perfil no sticky. |
| ≤ 560 px | Stats en 1 columna, timeline compacto, footer apilado. |
| ≤ 520 px | Marca compacta sin rol, enlaces de nav más densos. |

Las cuadrículas usan `auto-fit` + `minmax(min(100%, Npx), 1fr)`, por lo que se
adaptan sin media queries adicionales.

## 11. Estructura de carpetas

Se respetó la estructura solicitada para `Portfolio.Web`
(`Components`, `Layouts`, `Pages`, `Shared`, `Services`, `Models`, `Features`,
`Themes`, `Documentation`, `Assets`), con dos matices documentados:

- `Themes` y `Assets` viven bajo `wwwroot/` porque deben servirse como archivos
  estáticos (limitación de ASP.NET Core, no una decisión arbitraria).
- `Documentation` está dentro del proyecto Web tal y como pedía el encargo, y
  el README de la raíz enlaza a ella.

La herramienta de exportación vive en `tools/`, separada de la aplicación, con
responsabilidad única.

## 12. Recomendaciones futuras

1. **Tests automatizados**: proyecto `Portfolio.Web.Tests` (xUnit) para handlers,
   estrategias y factoría; un smoke test del exportador. Es la carencia más
   importante de la v1.
2. **Lighthouse CI** en el workflow: presupuesto de rendimiento/accesibilidad
   por PR.
3. **Integridad del CDN de Mermaid**: fijar versión con `integrity`/`crossorigin`
   o self-hostear el archivo en `wwwroot`.
4. **Link checker** en CI para los enlaces externos de contacto.
5. **CSP** (`Content-Security-Policy`) cuando exista hosting con cabeceras;
   GitHub Pages no permite cabeceras personalizadas.
6. **i18n**: implementado en la iteración de localización (ES en `/es`, EN en
   `/en`, raíz redirige a `/es`) con `ILanguageContext` + `ITranslator`; ver
   `localization.md`.
7. **Filtros como rutas**: convertir `?category=` en `/projects/{category}`
   para que los filtros funcionen también en GitHub Pages.
8. **Datos desacoplados**: mover los seeds a JSON/embedded resources si crecen.
9. **`GetByIdAsync`**: eliminar si no aparece consumidor en la v2.

## 13. Conclusión

La v1.0.0 alcanza el estándar de "portfolio como demostración de ingeniería":
simple donde debe serlo (sin frameworks, sin JS), riguroso donde aporta
(patrones con propósito, configuración validada, build sin advertencias) y
honesto en su documentación. Las recomendaciones de la sección 12 definen la
hoja de ruta técnica para la v1.1.
