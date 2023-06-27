using Application.Logging.Events;
using Domain.Error;
using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Logging.Commands
{
	public record LogInformationCommand(string Information, string ComponentName) : IRequest<Guid>
	{
		public class Handler : IRequestHandler<LogInformationCommand, Guid>
		{
			private readonly ITraceLogRepo _traceLogRepo;
			private readonly IUnitOfWork _unitOfWork;
			private readonly IPublisher _publisher;
			public Handler(
				ITraceLogRepo traceLogRepo,
				IUnitOfWork unitOfWork,
				IPublisher publisher)
			{
				_traceLogRepo = traceLogRepo;
				_unitOfWork = unitOfWork;
				_publisher = publisher;
			}
			public async Task<Guid> Handle(
				LogInformationCommand request,
				CancellationToken cancellationToken = default)
			{

				var traceLog = new TraceLog
				{
					TraceId = Guid.NewGuid(),
					Type = "Information",
					Message = request.Information,
					ComponentName = request.ComponentName,
					LoggedDate = DateTime.Now
				};

				var run = true;
				do
				{
					var log = await _unitOfWork.TraceLogs.GetAsNoTrackingAsync(traceLog.TraceId);
					if (log != null)
					{
						traceLog.TraceId = Guid.NewGuid();
					}
					else
					{
						run = false;
					}
				} while (run);


				await _traceLogRepo.CallLogExceptionAsync(traceLog);
				await _unitOfWork.SaveAsync();
				await _publisher.Publish(
					new TraceLogAddedEvent(traceLog.TraceId, traceLog.Message),
					cancellationToken);
				return traceLog.TraceId;
			}
		}
	}

}