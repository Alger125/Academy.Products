

namespace Academy.Products.Infrastructure.Persistence.Configuration;
    public class AppOptions
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
