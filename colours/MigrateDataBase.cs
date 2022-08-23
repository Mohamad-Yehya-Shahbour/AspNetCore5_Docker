using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace colours
{
    public static class MigrateDataBase
    {
        public static IHost Migrate<T>(this IHost webHost) where T: DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetService<T>();

                    db.Database.EnsureDeleted();
                    db.Database.GetPendingMigrations();
                    db.Database.Migrate();
                    
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while mograting the database...");
                }
            }
            return webHost;
        }
    }
}
