using Domain.DataTransferObjects;
using Domain.Models;

namespace Domain.Repositories
{
    public interface ITraceLogRepo : IBaseRepository<TraceLog, Guid>
    {
        Task CallLogExceptionAsync(TraceLog traceLog);
        IAsyncEnumerable<TraceLogDto> GetLastLogs(int numberOfLogs);
        IAsyncEnumerable<TraceLogDto> GetLastLogsInTimeSpan(
	        int numberOfLogs, DateTime from, DateTime to);


    }
}
