using System.Diagnostics;
using System.Xml.Linq;
using Domain.DataTransferObjects;
using Domain.Error;
using Domain.Models;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;



namespace Presistence.Repositories
{
    public class TraceLogRepo : BaseRepository<TraceLog, Guid>, ITraceLogRepo
    {

        private readonly IErrorCatcherService<TraceLog> _ecs;
        public CleanDbContext Db => Context as CleanDbContext;
        public TraceLogRepo(CleanDbContext context) : base(context)
        {
            _ecs = new ErrorCatcherService<TraceLog>();
        }

        public async Task CallLogExceptionAsync(TraceLog traceLog) 
            => await _ecs.ExecuteAsync( async() 
                => await LogExceptionAsync(traceLog));
        
        private async Task LogExceptionAsync(TraceLog traceLog)
            => await Db.TraceLogs.AddAsync(traceLog);

        public async IAsyncEnumerable<TraceLogDto> GetLastLogs(int numberOfLogs)
        {
	        var enumerator = LatestTraceLogsCompiledQuery(Db, numberOfLogs).GetAsyncEnumerator();
	        
	        try
	        {
		        while (await enumerator.MoveNextAsync())
		        {
			        var item = enumerator.Current;
			        yield return item;
				}
	        }
	        finally
	        {
		        await enumerator.DisposeAsync();
	        }
        }
        private static readonly Func<CleanDbContext, int, IAsyncEnumerable<TraceLogDto>>
	        LatestTraceLogsCompiledQuery =
		        EF.CompileAsyncQuery(
			        (CleanDbContext ctx, int numberOfLogs) => ctx.TraceLogs
				        .OrderByDescending(x => x.LoggedDate)
				        .Take(numberOfLogs)
				        .Select(log => new TraceLogDto
			                {
				                TraceId = log.TraceId,
				                ComponentName = log.ComponentName,
				                LoggedDate = log.LoggedDate,
				                Message = log.Message,
			                })
				        .AsNoTracking());
		public async IAsyncEnumerable<TraceLogDto> GetLastLogsInTimeSpan(
	        int numberOfLogs,DateTime from, DateTime to)
        {
	        await foreach (var logDto in TraceLogsInTimeSpanCompiledQuery(Db,from,to,numberOfLogs)) 
		        yield return logDto;
        }

		private static readonly Func<CleanDbContext, DateTime, DateTime, int,IAsyncEnumerable<TraceLogDto>> 
			TraceLogsInTimeSpanCompiledQuery = 
				EF.CompileAsyncQuery(
					(CleanDbContext ctx, DateTime from, DateTime to, int numberOfLogs) => ctx.TraceLogs
						.Where(log => log.LoggedDate > from && log.LoggedDate < to)
						.OrderByDescending(x => x.LoggedDate)
						.Take(numberOfLogs)
						.Select(log => new TraceLogDto
		                    {
                                TraceId = log.TraceId,
                                ComponentName = log.ComponentName,
                                LoggedDate = log.LoggedDate,
                                Message = log.Message,
		                    })
		                .AsNoTracking());
	}
}
