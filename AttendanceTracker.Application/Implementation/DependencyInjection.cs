using AttendanceTracker.Data.Implementation;
using AttendanceTracker.Domain.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace AttendanceTracker.Application.Implementation
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectOrchestration(this IServiceCollection services, string connectionString)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.InjectDataAccess(connectionString);

            services.AddTransient(_ => services);

            services.AddSingleton(GetHandlers());

            services.AddSingleton<IHandlerFactory, HandlerFactory>();

            services.AddSingleton<IOrchestrator, Orchestrator>();

            services.AddSingleton<IRandomCharacterFactory, RandomCharacterFactory>();

            return services;
        }

        static IEnumerable<Type> GetHandlers() => typeof(DependencyInjection).Assembly.GetTypes().Where(x => typeof(IHandler).IsAssignableFrom(x) && x.IsClass);
    }
}
