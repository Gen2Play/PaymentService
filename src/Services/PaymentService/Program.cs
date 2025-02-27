using Common.Logging;
using PaymentService.Extensions;
using PaymentService.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Starting Payment API up");

try
{
    builder.Environment.ApplicationName = builder.Configuration["ApplicationName"];
    builder.Host.UseSerilog(SeriLogger.Configure);
    builder.Host.AddAppConfiguration();
    // Add services to the container.
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();
    app.UseInfrastructure();

    app.MigrateDatabase<PaymentContext>((context, _) => {})
        .Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;
    
    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information("Shut down Payment API complete");
    Log.CloseAndFlush();
}