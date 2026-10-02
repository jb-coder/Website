# LanguageSwitcher

## Responsabilidad

Cambiar entre español e inglés manteniendo la página actual y su query string.
Es un enlace real, no un control JavaScript, para que funcione en el sitio
estático y sea indexable.

## Inputs

Ninguno. Obtiene el estado de `ILanguageContext` y las etiquetas accesibles de
`ITranslator`.

## Outputs

- `<a>` con `hreflang` del idioma destino y `aria-label` localizado
  ("Ver esta página en inglés" / "View this page in Spanish").
- Indicador visual `ES / EN` con el idioma activo resaltado.

## Dependencias

- `ILanguageContext` (`SwitchHref`, `Language`, `OtherLanguage`).
- `ITranslator` (`Language.ViewInEnglish`, `Language.ViewInSpanish`).
- `Icon`.

## Casos de uso

- Header, junto a la navegación principal, en todas las páginas.

## Comportamiento con query string

| Página actual | Enlace del conmutador |
| --- | --- |
| `/projects` | `en/projects` |
| `/en/projects` | `projects` |
| `/projects?category=Mobile` | `en/projects?category=Mobile` |
| `/en/projects?category=Mobile` | `projects?category=Mobile` |
