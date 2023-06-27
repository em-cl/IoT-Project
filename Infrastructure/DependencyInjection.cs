using FluentValidation;
using Infrastructure.Config;
using Infrastructure.Services.MailServices;
using MailKit.Net.Smtp;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
	        this IServiceCollection services, 
	        IConfigurationSection mailSettings)
        {
	        var assembly = typeof(DependencyInjection).Assembly;

	        services.AddMediatR(configuration =>
	        {
		        configuration.RegisterServicesFromAssembly(assembly);
		        configuration.NotificationPublisher = new TaskWhenAllPublisher();
	        });

	        services.AddValidatorsFromAssembly(assembly);

	        services.Configure<MailConfig>(config =>
	        {
		        config.Host = mailSettings["Host"]!;
		        config.UserName = mailSettings["UserName"]!;
				config.Password = mailSettings["Password"]!;
			});

			services.AddTransient<ISmtpClient,SmtpClient>();
	        services.AddTransient<IMailSenderService,MailSenderService>();
			return services;
        }
    }
}
