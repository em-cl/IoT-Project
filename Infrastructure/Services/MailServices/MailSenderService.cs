using Infrastructure.Config;
using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services.MailServices
{
	// Källa: Anders Andreén
	public class MailSenderService : IMailSenderService
	{
		private readonly MailConfig _mailConfig;

		public MailSenderService(
			IOptions<MailConfig> options)
		{
			_mailConfig = options.Value;
		}

		public async Task<bool> SendAsync(
			string emailTo, string subject, string body)
		{
			try
			{
				if (string.IsNullOrEmpty(emailTo) ||
					string.IsNullOrEmpty(subject) ||
					string.IsNullOrEmpty(body))
					return false;

				var source = new CancellationTokenSource(
					TimeSpan.FromDays(1));

				var message = BuildMessage(emailTo, subject, body);

				using var client = new SmtpClient();
				
				client.ServerCertificateValidationCallback =
					(s, c, h, e) => true;
				
				client.CheckCertificateRevocation = false;
				
				await client.ConnectAsync(
					_mailConfig.Host, 587, SecureSocketOptions.StartTls, source.Token);
				
				await client.AuthenticateAsync(
					_mailConfig.UserName, _mailConfig.Password, source.Token);
				
				await client.SendAsync(
					message, source.Token);
				
				await client.DisconnectAsync(true, source.Token);


				
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}
		}

		private MimeMessage BuildMessage(string emailTo, string subject, string body)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(
					"Pico W Dashboard", "mtbi.groupmaker@gmail.com"));

			message.To.Add(new MailboxAddress("User", emailTo));
			message.Subject = subject;

			var bodyBuilder = new BodyBuilder
			{
				HtmlBody = body
			};

			message.Body = bodyBuilder.ToMessageBody();
			return message;
		}
	}
}
