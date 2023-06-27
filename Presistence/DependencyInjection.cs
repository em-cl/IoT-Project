using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Repositories;

namespace Presistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresistence(
	        this IServiceCollection services, 
	        string connectionString)
        {
			//Repos
			services.AddDbContext<CleanDbContext>(optionsBuilder =>
			{
				optionsBuilder.UseLazyLoadingProxies();
				optionsBuilder.EnableSensitiveDataLogging(false);
				optionsBuilder.UseSqlServer(
					connectionString,
					b => b.MigrationsAssembly("WebAPI"));

			});
			services.AddTransient<IUnitOfWork, UnitOfWork>();
	        services.AddTransient<ITraceLogRepo, TraceLogRepo>();
	        services.AddTransient<IMeasurementRepo, MeasurementRepo>();

            return services;
            
        }
    }
}
