using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DataTransferObjects;
using Domain.Entities;
using Domain.Models;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Repositories
{
    public class MeasurementRepo : BaseRepository<Measurement, Guid>, IMeasurementRepo 
    {

        private readonly IErrorCatcherService<TraceLog> _ecs;
        public CleanDbContext Db => Context as CleanDbContext;
        public MeasurementRepo(CleanDbContext context) : base(context)
        {
            _ecs = new ErrorCatcherService<TraceLog>();
        }

        public async Task CallSaveMeasurements(
            List<MeasurementDto> measurementDtos)
            => await _ecs.ExecuteAsync(
                async () => await SaveMeasurements(measurementDtos));
        private async Task SaveMeasurements(List<MeasurementDto> measurementDtos)
        {
            Db.AddRange(Measurement.MapToModels(measurementDtos));
            await Db.SaveChangesAsync();
            Db.ChangeTracker.Clear();
        }

        public async IAsyncEnumerable<(TraceLogDto?,Measurement?)> GetLastHistoryAsyncEnumerable(
	        int numberOfMeasurements)
        {
	        
	        var enumerator1 = LastMeasurementsCq(Db, numberOfMeasurements).GetAsyncEnumerator();
	        try
	        {
		        while (await enumerator1.MoveNextAsync())
		        {
			        var item = enumerator1.Current;
			        
			        yield return (null, item);
		        }
	        }
	        finally
	        {
		        await enumerator1.DisposeAsync();
	        }
	        var enumerator2 = LatestTraceLogsCompiledQuery(Db, numberOfMeasurements).GetAsyncEnumerator();
	        try
	        {
		        while (await enumerator2.MoveNextAsync())
		        {
			        var item = enumerator2.Current;
			        yield return (item, null);
		        }
	        }
	        finally
	        {
		        await enumerator2.DisposeAsync();
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
		public async IAsyncEnumerable<Measurement> GetLastMeasurementsAsyncEnumerable(
            int numberOfMeasurements)
        {
	        var enumerator1 = LastMeasurementsCq(Db, numberOfMeasurements).GetAsyncEnumerator();
	        try
	        {
		        while (await enumerator1.MoveNextAsync())
		        {
			        var item = enumerator1.Current;
			        yield return item;
		        }
	        }
	        finally
	        {
		        await enumerator1.DisposeAsync();
	        }
        }

        public static readonly Func<CleanDbContext, int, IAsyncEnumerable<Measurement>>
	        LastMeasurementsCq =
		        EF.CompileAsyncQuery((CleanDbContext db, int numberOfMeasurements) => db.Measurements
			        .OrderByDescending(m => m.Time)
			        .Take(numberOfMeasurements)
			        .AsNoTracking());
        public async IAsyncEnumerable<Measurement> GetLastMeasurementsAsyncEnumerable2(
	        int numberOfMeasurements, DateTime from, DateTime To)
        {
	        await foreach (var measurement in LastMeasurementsInTimeSpanCq(Db, from, To, numberOfMeasurements))
	        {
		        yield return measurement;
	        }
        }
          
        public static readonly Func<CleanDbContext, DateTime, DateTime, int, IAsyncEnumerable<Measurement>>
	        LastMeasurementsInTimeSpanCq =
		        EF.CompileAsyncQuery(
			        (CleanDbContext db, DateTime from, DateTime to, int numberOfMeasurements) => db.Measurements
				        .Where(m=>m.Time>from && m.Time<to)
				        .OrderByDescending(m => m.Time)
				        .Take(numberOfMeasurements)
				        .AsNoTracking());
	}
}
