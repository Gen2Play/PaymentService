using Common.Logging;
using NotificationService.Extensions;
using Serilog;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(SeriLogger.Configure);

Log.Information("Starting Notifications API up");

try
{
    // Add services to the container.
    builder.Environment.ApplicationName = builder.Configuration["ApplicationName"];
    builder.Host.AddAppConfiguration();
    builder.Services.ConfigureServices();
    builder.Services.ConfigureRedis(builder.Configuration);
    builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
    builder.Services.AddDiscoveryClient(builder.Configuration);
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
    

    // app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    
    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;
    
    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information("Shut down Notifications API complete");
    Log.CloseAndFlush();
}