using Application.Logging.Events;
using Domain.Error;
using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Application.Logging.Commands
{
    public record LogErrorCommand(Exception Exception, string ComponentName) : IRequest<Guid>
    {
        public class Handler : IRequestHandler<LogErrorCommand, Guid>
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
                LogErrorCommand request,
                CancellationToken cancellationToken = default)
            {

                var traceLog = new TraceLog
                {
                    TraceId = Guid.NewGuid(),
                    Type = "Error",
                    Message = request.Exception
                        .GetAllMessages()
                        .ExtraDebugInfo(),
                    ComponentName = request.ComponentName,
                    LoggedDate = DateTime.Now
                };

                var log = await _unitOfWork.TraceLogs.GetAsNoTrackingAsync(traceLog.TraceId);

                if (log == null)
	                return Guid.Empty;

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
