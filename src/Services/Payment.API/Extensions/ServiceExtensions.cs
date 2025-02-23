using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Payment.API.Persistence;
using Payment.API.Repositories;
using Payment.API.Repositories.Interfaces;
using Payment.API.Services;
using Payment.API.Services.Interfaces;

namespace Payment.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureProductContext(configuration);
            services.AddInfrastructureServices();
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

            return services;
        }

        private static IServiceCollection ConfigureProductContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            var builder = new NpgsqlConnectionStringBuilder(connectionString);

            services.AddDbContext<PaymentContext>(options =>
                options.UseNpgsql(builder.ConnectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly("Payment.API");
                }));

            return services;
        }

        private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryQueryBase<,,>))
                .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
                .AddScoped<IPaymentRepository, PaymentRepository>()
                .AddScoped<IPaymentService, PaymentService>();
        }
    }
}