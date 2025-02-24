using Microsoft.EntityFrameworkCore;

namespace PaymentService.Extensions
{
    public static class Hostextensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating postgresql database");
                    ExecuteMigrations(context);
                    logger.LogInformation("Migrated postgresql database");
                    InvokeSeeder(seeder, context, services);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "An error occurred while migrating the postgresql database");
                }
            }

            return host;
        }

        private static void ExecuteMigrations<TContext>(this TContext context)
            where TContext : DbContext
        {
            if (!context.Database.CanConnect())
            {
                context.Database.EnsureCreated();
            }
            
            context.Database.Migrate();
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
            IServiceProvider services) where TContext : DbContext
        {
            seeder(context, services);
        }
    }
}