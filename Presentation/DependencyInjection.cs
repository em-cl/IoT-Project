using FluentValidation;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
			services.AddRazorPages();
			services.AddServerSideBlazor();
			var assembly = typeof(DependencyInjection).Assembly;

			services.AddMediatR(configuration =>
			{
				configuration.RegisterServicesFromAssembly(assembly);
				configuration.NotificationPublisher = new TaskWhenAllPublisher();
			});

			services.AddValidatorsFromAssembly(assembly);
			return services;
        }
    }
}
