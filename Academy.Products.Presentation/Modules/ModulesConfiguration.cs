using Microsoft.AspNetCore.Builder;
 
namespace Academy.Products.Presentation.Modules;

public class ModulesConfiguration
{
    public static void Configure(WebApplication app)
    {
        app.AddProductModules();
    }
    
    
}
