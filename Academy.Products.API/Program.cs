using Academy.Products.Application;
using Academy.Products.Presentation.Modules;
using Academy.Products.Infrastructure;
using Academy.Products.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
 
// Agregar servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructureService();


// Aquí puedes registrar tus servicios de Application, Infrastructure, etc.
// builder.Services.AddScoped();

var app = builder.Build();
InitializeDatabase(app);


void InitializeDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Aplica migraciones pendientes
    dbContext.Database.Migrate();
}


// Configurar Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Academy.Products API v1");
        //options.RoutePrefix = string.Empty; // Para que se muestre en la raíz: https://localhost:5001/
    });
}

ModulesConfiguration.Configure(app);

app.UseHttpsRedirection();
 
//app.MapGet("/", () => "Academy.Products API - .NET 8");
 
app.Run();