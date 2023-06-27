using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.PicoWCommunication.Events
{
	public record BatchSavedEvent(List<Measurement> Measurements) : INotification;
}
