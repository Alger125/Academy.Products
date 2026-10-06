# Instrucciones para agentes de IA

Proyecto: Academy.Products (.NET 8, Clean Architecture, CQRS con MediatR).

Antes de cualquier tarea:
1. Ejecuta `git branch --show-current` e identifica la rama actual.
2. Lee la ficha de esa rama en `BRANCHES.md`.
3. Respeta las reglas de esa ficha (qué se permite, qué está prohibido).

Reglas globales:
- Nunca hagas push directo a `develop`, `qa`, `prd` ni `main`. Todo va por Pull Request.
- Dependencias entre capas: API → Application → Domain; Infrastructure → Domain/Application.
  Domain NO referencia a ningún otro proyecto ni a EF Core, HttpClient, etc.
- Orden de implementación: Domain → Infrastructure → Application → API → Tests.
- Antes de proponer un PR ejecuta `dotnet build` y `dotnet test`.
- Commits en formato Conventional Commits (`feat:`, `fix:`, `docs:`, `test:`, `refactor:`).
