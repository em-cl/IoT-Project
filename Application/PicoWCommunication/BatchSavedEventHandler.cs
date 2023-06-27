using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.PicoWCommunication.Events;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.PicoWCommunication
{
	internal sealed class BatchSavedEventHandler : INotificationHandler<BatchSavedEvent>
	{
		public Task Handle(BatchSavedEvent notification, CancellationToken cancellationToken = default)
		{
			Console.WriteLine("Batch data saved processing output for presentation.");
			Debug.WriteLine("Batch data saved processing output for presentation.");
			var counter = 0;
			foreach (var measurement in notification.Measurements)
			{
				Console.Write(counter +" Humidity: " + measurement.Humidity +", ");
				Console.Write(counter + " Temperature: " + measurement.Temperature+", ");
				Console.Write(counter + " Date: " + measurement.Time + "\n");
				counter++;
			}

			counter = 0;
			foreach (var cachedItem in PicoWDataConversion.FifoQueue.ToArray())
			{
				Console.Write(counter + " Humidity: " + cachedItem.Humidity + ", ");
				Console.Write(counter + " Temperature: " + cachedItem.Temperature + ", ");
				Console.Write(counter + " Date: " + cachedItem.Time + "\n");
				counter++;
			}

			Console.WriteLine("Batch processed sending to presentation layer.");
			Debug.WriteLine("Batch processed sending to presentation layer.");
			return Task.CompletedTask;
		}
	}

}
