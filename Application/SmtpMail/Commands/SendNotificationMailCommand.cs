using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Logging.Commands;
using Domain.Entities;
using Domain.Error;
using Domain.Repositories;
using Infrastructure.Services.MailServices;
using MediatR;

namespace Application.SmtpMail.Commands
{
	public record SendNotificationMailCommand(
		string EmailTo, string Subject, string Body) : IRequest<string>
	{
		public class Handler : IRequestHandler<SendNotificationMailCommand, string>
		{
			private readonly IMailSenderService _mailSenderService;
			public Handler(IMailSenderService mailSenderService)
			{
				_mailSenderService = mailSenderService;
			}

			public async Task<string> Handle(
				SendNotificationMailCommand request,
				CancellationToken cancellationToken = default)
			{
				try
				{
					var result = await _mailSenderService.SendAsync(
						request.EmailTo, request.Subject, request.Body);

					if (result != true) throw new ArgumentNullException();

					return "Mail notification sent";
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.ToString().ExtraDebugInfo());
					Console.WriteLine(ex.ToString().ExtraDebugInfo());
					return "Internal error logged";
				}
			}
		}
	}
}
