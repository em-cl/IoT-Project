using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Logging.Events;
using MediatR;

namespace Application.Logging
{
	internal sealed class TraceLogAddedEventHandler
		: INotificationHandler<TraceLogAddedEvent>
	{
		public Task Handle(
			TraceLogAddedEvent notification, 
			CancellationToken cancellationToken = default)
		{
			Console.WriteLine($"Logged id:{notification.TraceLogId}\r\n{notification.Message}");
			Debug.WriteLine($"Logged id:{notification.TraceLogId}\r\n{notification.Message}");
			return Task.CompletedTask;
		}
	}
}
