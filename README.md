# 🛒 Academy.Products

Sistema de gestión de catálogo de productos construido con **.NET 8**, siguiendo los principios de **Clean Architecture** y el patrón **CQRS**.

---

## 📚 Tabla de Contenidos

1. [¿Qué es este proyecto?](#qué-es-este-proyecto)
2. [Arquitectura: la gran imagen](#arquitectura-la-gran-imagen)
3. [Estructura de carpetas explicada](#estructura-de-carpetas-explicada)
4. [Flujo de una petición de punta a punta](#flujo-de-una-petición-de-punta-a-punta)
5. [¿Por qué esta estructura?](#por-qué-esta-estructura)
6. [Historias de usuario (qué vamos a construir)](#historias-de-usuario)
7. [Flujo de ramas (Git)](#flujo-de-ramas-git)
8. [Cómo levantar el proyecto](#cómo-levantar-el-proyecto)
9. [Guía de contribución paso a paso](#guía-de-contribución-paso-a-paso)

---

## ¿Qué es este proyecto?

`Academy.Products` es una **API REST** que permite:

- A los **clientes**: buscar, filtrar y visualizar productos con imagen, descripción y precio.
- A los **administradores**: agregar, editar y eliminar productos del inventario.

El backend está escrito en **C# con .NET 8** y expone endpoints HTTP estándar con códigos de respuesta bien definidos.

---

## Arquitectura: la gran imagen

Este proyecto sigue **Clean Architecture** (también conocida como arquitectura de capas). La regla principal es simple:

> **Las capas internas no saben nada de las capas externas. Las dependencias siempre apuntan hacia adentro.**

```
┌────────────────────────────────────────────────────┐
│                  API (entrada HTTP)                │  ← Capa más externa
│              Academy.Products.API                  │
└────────────────────────┬───────────────────────────┘
                         │ usa
┌────────────────────────▼───────────────────────────┐
│           Application (casos de uso / CQRS)        │  ← Orquestador
│          Academy.Products.Application              │
└──────────────┬─────────────────────┬───────────────┘
               │ usa                 │ usa
┌──────────────▼──────────┐  ┌───────▼──────────────┐
│  Domain (reglas de      │  │ Infrastructure       │  ← Detalles técnicos
│  negocio / entidades)   │  │ (base de datos, etc.)│
│  Academy.Products.Domain│  │ Academy.Products.    │
│                         │  │ Infrastructure       │
└─────────────────────────┘  └──────────────────────┘

┌────────────────────────────────────────────────────┐
│                Tests (pruebas)                     │  ← Capa transversal
│            Academy.Products.Tests                  │
└────────────────────────────────────────────────────┘
```

---

## Estructura de carpetas explicada

```
Academy.Products/
│
├── Academy.Products.sln                  # Solución: el "paraguas" que agrupa todos los proyectos
│
├── Academy.Products.API/                 # 🌐 Proyecto web - recibe peticiones HTTP
│   ├── Academy.Products.API.csproj
│   ├── appsettings.json                  # Configuración general (conexiones, logging, etc.)
│   ├── appsettings.Development.json      # Overrides para entorno local
│   └── Academy.Products.API.http         # Archivo para probar endpoints desde VS/Rider
│
├── Academy.Products.Application/         # 🧠 Lógica de aplicación - casos de uso
│   ├── Academy.Products.Application.csproj
│   └── Class1.cs                         # (placeholder - aquí irán Commands, Queries, Handlers)
│
├── Academy.Products.Domain/              # 💎 Corazón del negocio - entidades y reglas
│   ├── Academy.Products.Domain.csproj
│   └── Class1.cs                         # (placeholder - aquí irán Product, Category, etc.)
│
├── Academy.Products.Infrastructure/      # 🔧 Detalles técnicos - BD, archivos, externos
│   ├── Academy.Products.Infrastructure.csproj
│   └── Class1.cs                         # (placeholder - aquí irá el repositorio con EF Core)
│
└── Academy.Products.Tests/               # ✅ Pruebas unitarias con xUnit
    ├── Academy.Products.Tests.csproj
    ├── UnitTest1.cs                       # (placeholder - aquí irán los tests reales)
    └── xunit.runner.json                  # Configuración del runner de xUnit
```

### ¿Qué va en cada proyecto? (La guía concreta)

#### `Academy.Products.Domain` — El corazón 💎

Este proyecto **no depende de nada más** del solution. Aquí viven las reglas de negocio puras.

```csharp
// Ejemplo de lo que irá aquí:

// Entidades
public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Category { get; private set; }

    // Reglas de negocio: el precio nunca puede ser negativo
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0) throw new DomainException("El precio no puede ser negativo.");
        Price = newPrice;
    }
}

// Interfaces de repositorios (el contrato, no la implementación)
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> SearchAsync(string term);
}
```

> 🔑 **Regla de oro**: si alguien te muestra código del Domain que hace `using EntityFramework` o `using HttpClient`, algo está mal. El Domain no sabe nada de tecnología.

---

#### `Academy.Products.Infrastructure` — Los detalles técnicos 🔧

Aquí se **implementan** las interfaces definidas en Domain. Esta capa sabe cómo hablar con una base de datos, un servicio externo, etc.

```csharp
// Ejemplo de lo que irá aquí:

// Implementación concreta del repositorio usando Entity Framework
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
        => await _context.Products.FindAsync(id);

    public async Task<IEnumerable<Product>> SearchAsync(string term)
        => await _context.Products
            .Where(p => p.Name.Contains(term))
            .ToListAsync();
}
```

---

#### `Academy.Products.Application` — El orquestador con CQRS 🧠

Aquí implementamos el patrón **CQRS (Command Query Responsibility Segregation)**:

- **Query** = leer datos (sin modificar estado)
- **Command** = escribir/modificar datos

```csharp
// QUERY: "Dame todos los productos que contengan 'laptop'"
public record SearchProductsQuery(string Term) : IRequest<IEnumerable<ProductDto>>;

public class SearchProductsHandler : IRequestHandler<SearchProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _repository;

    public SearchProductsHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(SearchProductsQuery request, CancellationToken ct)
    {
        var products = await _repository.SearchAsync(request.Term);
        return products.Select(p => new ProductDto(p.Id, p.Name, p.Price));
    }
}

// COMMAND: "Agrega este nuevo producto al inventario"
public record CreateProductCommand(string Name, decimal Price, string Category) : IRequest<Guid>;
```

---

#### `Academy.Products.API` — La puerta de entrada 🌐

Expone los endpoints HTTP. Su única responsabilidad es recibir la request, delegarla a Application, y devolver la respuesta con el código HTTP correcto.

```csharp
// Ejemplo de controller/endpoint:
app.MapGet("/products/search", async (string term, ISender sender) =>
{
    var result = await sender.Send(new SearchProductsQuery(term));
    return Results.Ok(result);
});

app.MapPost("/products", async (CreateProductCommand command, ISender sender) =>
{
    var id = await sender.Send(command);
    return Results.Created($"/products/{id}", new { id });
});
```

---

#### `Academy.Products.Tests` — Las pruebas ✅

Prueba la lógica de negocio de forma aislada. Usa **xUnit v3**.

```csharp
public class ProductTests
{
    [Fact]
    public void UpdatePrice_WhenNegative_ShouldThrowDomainException()
    {
        // Arrange
        var product = new Product("Laptop", 1000m, "Electronics");

        // Act & Assert
        Assert.Throws<DomainException>(() => product.UpdatePrice(-50m));
    }
}
```

---

## Flujo de una petición de punta a punta

Ejemplo: el cliente busca productos con la palabra "laptop".

```
1. Cliente HTTP
   GET /products/search?term=laptop
          │
          ▼
2. Academy.Products.API
   Recibe la request, construye SearchProductsQuery("laptop")
   y la envía al mediador (MediatR)
          │
          ▼
3. Academy.Products.Application
   SearchProductsHandler recibe la Query,
   llama a IProductRepository.SearchAsync("laptop")
          │
          ▼
4. Academy.Products.Infrastructure
   ProductRepository ejecuta la consulta SQL real
   contra la base de datos
          │
          ▼
5. Resultado sube de vuelta por la cadena
   Infrastructure → Application (mapea a DTOs) → API → HTTP 200 OK con JSON
```

---

## ¿Por qué esta estructura?

| Beneficio | Explicación |
|---|---|
| **Testabilidad** | El Domain y Application no dependen de frameworks, son fáciles de probar con mocks. |
| **Independencia de BD** | Puedes cambiar de SQL Server a PostgreSQL sin tocar Domain ni Application. |
| **Claridad** | Cada proyecto tiene una sola responsabilidad. Sabes exactamente dónde buscar cada cosa. |
| **Escalabilidad** | Es fácil agregar nuevos casos de uso sin romper los existentes. |

---

## Historias de usuario

Las funcionalidades a desarrollar según la épica:

| # | Historia | Estado |
|---|---|---|
| 1 | Como **cliente**, quiero buscar productos por nombre o categoría | ⬜ Pendiente |
| 2 | Como **cliente**, quiero filtrar productos por precio | ⬜ Pendiente |
| 3 | Como **administrador**, quiero agregar/editar/eliminar productos | ⬜ Pendiente |
| 4 | Como **cliente**, quiero visualizar productos con imagen, descripción y precio | ⬜ Pendiente |

---

## Flujo de ramas (Git)

El proyecto usa un flujo de **ramas por ambiente**. Nunca se hace push directo, siempre por Pull Request:

```
feature/mi-feature
       │
       │  PR ──► develop   (integración continua)
                   │
                   │  PR ──► qa      (pruebas de calidad)
                               │
                               │  PR ──► prd    (pre-producción)
                                           │
                                           │  PR ──► main  (producción estable ✅)
```

### Convención para nombrar ramas

```bash
# Features nuevas
git checkout -b feature/buscar-productos-por-nombre

# Corrección de bugs
git checkout -b fix/precio-negativo-en-creacion

# Hotfix urgente en producción
git checkout -b hotfix/error-critico-en-api
```

---

## Cómo levantar el proyecto

### Pre-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 / JetBrains Rider / VS Code

### Pasos

```bash
# 1. Clonar el repositorio
git clone <url-del-repo>
cd Academy.Products

# 2. Restaurar dependencias
dotnet restore

# 3. Compilar la solución completa
dotnet build

# 4. Correr los tests
dotnet test

# 5. Levantar la API
cd Academy.Products.API
dotnet run
```

La API estará disponible en: `http://localhost:5021`

La documentación Swagger estará en: `http://localhost:5021/swagger`

---

## Guía de contribución paso a paso

1. **Crea tu rama** desde `develop`:
   ```bash
   git checkout develop
   git pull origin develop
   git checkout -b feature/nombre-descriptivo
   ```

2. **Implementa** siguiendo el orden de capas:
   - Empieza por **Domain** (entidad + interfaz de repositorio)
   - Luego **Infrastructure** (implementación del repositorio)
   - Luego **Application** (Command o Query + Handler)
   - Por último **API** (endpoint)
   - No olvides los **Tests**

3. **Asegúrate** de que los tests pasen antes de abrir el PR:
   ```bash
   dotnet test
   ```

4. **Abre un Pull Request** hacia `develop` con una descripción clara de qué hace y por qué.
