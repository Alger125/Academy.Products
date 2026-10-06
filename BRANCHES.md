# Guía de ramas

## Mapa del flujo

`feature/*` | `fix/*`  ──PR──►  `develop`  ──PR──►  `qa`  ──PR──►  `prd`  ──PR──►  `main`
`hotfix/*`  ──PR──►  `main` (y luego se propaga hacia atrás a `prd`, `qa` y `develop`)

Regla: el código solo avanza hacia la derecha mediante Pull Request. Nunca se hace push directo.

---

## `main` — Producción estable
- **Propósito:** código que está (o va a estar) en producción.
- **Recibe PRs desde:** `prd` y `hotfix/*`.
- **Se puede:** solo fusionar PRs aprobados.
- **Prohibido:** commits directos, desarrollar aquí, fusionar sin CI en verde.
- **Calidad exigida:** build OK, todos los tests pasan, al menos 1 aprobación.
- **Instrucción para el agente:** si estás en esta rama, NO modifiques código. Solo lectura y consulta. Para cambiar algo, crea una rama `feature/*`, `fix/*` o `hotfix/*`.

## `prd` — Pre-producción
- **Propósito:** validación final antes de producción, con configuración lo más parecida posible a la real.
- **Recibe PRs desde:** `qa`.
- **Envía PRs hacia:** `main`.
- **Se puede:** ajustes de configuración de ambiente si hace falta, vía PR.
- **Prohibido:** features nuevas, commits directos.
- **Instrucción para el agente:** no agregues funcionalidades. Si encuentras un bug, documéntalo y propone un `fix/*` desde `develop`.

## `qa` — Pruebas de calidad
- **Propósito:** el equipo de QA valida las historias de usuario terminadas.
- **Recibe PRs desde:** `develop`.
- **Envía PRs hacia:** `prd`.
- **Se puede:** fusionar solo historias completas y probadas.
- **Prohibido:** código a medias, commits directos.
- **Instrucción para el agente:** si se detecta un defecto, no lo corrijas aquí. Crea `fix/*` desde `develop` y vuelve a promover el cambio.

## `develop` — Integración
- **Propósito:** rama de integración continua donde se juntan todas las features.
- **Recibe PRs desde:** `feature/*` y `fix/*`.
- **Envía PRs hacia:** `qa`.
- **Se puede:** fusionar trabajo terminado con tests pasando.
- **Prohibido:** commits directos, código que rompa el build.
- **Instrucción para el agente:** esta es la rama base. Toda rama nueva de trabajo se crea desde aquí (excepto `hotfix/*`). Antes de crear una rama: `git checkout develop && git pull origin develop`.

## `feature/*` — Funcionalidad nueva
- **Nombre:** `feature/descripcion-corta-en-kebab-case` (ej. `feature/buscar-productos-por-nombre`).
- **Se crea desde:** `develop`. **Se fusiona en:** `develop` vía PR.
- **Alcance:** una sola historia de usuario por rama.
- **Se puede:** modificar cualquier capa siguiendo el orden Domain → Infrastructure → Application → API → Tests.
- **Prohibido:** mezclar varias historias, tocar archivos no relacionados.
- **Instrucción para el agente:** esta es la rama donde SÍ escribes código. Implementa solo lo que pide la historia, agrega tests y ejecuta `dotnet test` antes del PR.

## `fix/*` — Corrección de bug
- **Nombre:** `fix/descripcion-del-bug` (ej. `fix/precio-negativo-en-creacion`).
- **Se crea desde:** `develop`. **Se fusiona en:** `develop` vía PR.
- **Se puede:** cambios mínimos que corrijan el defecto, más un test que lo reproduzca.
- **Prohibido:** refactors grandes o features nuevas.
- **Instrucción para el agente:** primero escribe un test que falle, luego corrige, luego verifica que pase.

## `hotfix/*` — Urgencia en producción
- **Nombre:** `hotfix/descripcion-del-error` (ej. `hotfix/error-critico-en-api`).
- **Se crea desde:** `main`. **Se fusiona en:** `main` vía PR y después se propaga a `prd`, `qa` y `develop`.
- **Se puede:** únicamente el cambio mínimo necesario para resolver el incidente.
- **Prohibido:** cualquier cosa que no sea el arreglo urgente.
- **Instrucción para el agente:** cambio lo más pequeño posible, con test, y recuerda al usuario que debe propagar el fix a las demás ramas.

---

## Estado actual del código (importante para el agente)
Las funcionalidades requeridas inicialmente ya fueron implementadas siguiendo el patrón de Clean Architecture y MediatR. Los placeholders (`Class1.cs`) originales deben ser limpiados.

## Historias de usuario y su estado
| # | Historia | Rama | Estado |
|---|---|---|---|
| 1 | Buscar productos por nombre o categoría | `feature/productsDetails/ECA-5` | ✅ Completado |
| 2 | Filtrar productos por precio | `feature/productsDetails/ECA-5` | ✅ Completado |
| 3 | Admin: agregar/editar/eliminar productos | `feature/productsDetails/ECA-5` | ✅ Completado |
| 4 | Visualizar producto con imagen, descripción y precio | `feature/productsDetails/ECA-5` | ✅ Completado |
