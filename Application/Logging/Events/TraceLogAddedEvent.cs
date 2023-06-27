using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Logging.Events
{
	public record TraceLogAddedEvent(Guid TraceLogId, string Message) : INotification;

}
