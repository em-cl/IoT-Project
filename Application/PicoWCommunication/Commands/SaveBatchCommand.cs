using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Logging.Events;
using Application.PicoWCommunication.Events;
using Domain.DataTransferObjects;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Identity.Json;

namespace Application.PicoWCommunication.Commands
{
	public record SaveBatchCommand (string Data) : IRequest<IEnumerable<Measurement>>
	{
		public class Handler : IRequestHandler<SaveBatchCommand, IEnumerable<Measurement>>
		{

			private readonly IUnitOfWork _unitOfWork;
			private readonly IPublisher _publisher;
			public Handler(
				IUnitOfWork unitOfWork,
				IPublisher publisher)
			{
				_unitOfWork = unitOfWork;
				_publisher = publisher;
			}
			public async Task<IEnumerable<Measurement>> Handle(
				SaveBatchCommand request, 
				CancellationToken cancellationToken = default)
			{
				if(string.IsNullOrEmpty(request.Data)) 
					return Enumerable.Empty<Measurement>();
				
				var measurements = Measurement
					.MapToModels(JsonSerializer
						.Deserialize<IEnumerable<MeasurementDto>>(request.Data) 
					             ?? Enumerable.Empty<MeasurementDto>()).ToList();

				if (measurements == null || !measurements.Any()) 
					return Enumerable.Empty<Measurement>();


				var newData = new List<Measurement>();
				foreach (var measurement in measurements)
				{
					if (await _unitOfWork.Measurements
						    .GetAsNoTrackingAsync(
							    filter: m => m.Time == measurement.Time) != null)
						continue;
					else
						newData.Add(measurement);
				}

				measurements.Clear();

				if (newData == null || !newData.Any()) 
					return Enumerable.Empty<Measurement>();
				
				//uncomment to save
				//await _unitOfWork.Measurements.AddRangeAsync(newData);
				//
				//await _unitOfWork.SaveAsync();

				_unitOfWork.ClearChangeTracker();

				await _publisher.Publish(new BatchSavedEvent(newData), cancellationToken);

				return newData;
			}
		}
	}
}
