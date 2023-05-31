using AttendanceTracker.Data.Implementation;
using AttendanceTracker.Domain.Factories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace AttendanceTracker.Application.Implementation
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection InjectOrchestration(this IServiceCollection services, string connectionString)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Call DependencyInjection for DataAccess Layer
            services.InjectDataAccess(connectionString);

            // Inject Dependencies For IOrchestrator (Mediator Pattern)
            services.AddTransient(_ => services);
            services.AddSingleton(GetHandlers());
            services.AddSingleton<IHandlerFactory, HandlerFactory>();
            services.AddSingleton<IOrchestrator, Orchestrator>();

            // Inject Domain Interfaces/Dependencies
            services.AddSingleton<IRandomCharacterFactory, RandomCharacterFactory>();
            services.AddSingleton<IRandomStringFactory, RandomStringFactory>();

            return services;
        }

        internal static IEnumerable<Type> GetHandlers() => typeof(DependencyInjection).Assembly.GetTypes().Where(x => typeof(IHandler).IsAssignableFrom(x) && x.IsClass);
    }
}
