using Microsoft.Extensions.DependencyInjection;

namespace AttendanceTracker.Data.Implementation
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection InjectDataAccess(this IServiceCollection services, string connectionString)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IDbConnectionFactory>(new DbConnectionFactory(connectionString));

            services.AddScoped<IDataAccess, DataAccess>();

            return services;
        }
    }
}
