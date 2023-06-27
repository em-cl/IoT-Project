using Domain.DataTransferObjects;
using Domain.Entities;

namespace Domain.Repositories;

public interface IMeasurementRepo : IBaseRepository<Measurement, Guid>
{
	Task CallSaveMeasurements(List<MeasurementDto> measurementDtos);

	IAsyncEnumerable<Measurement> GetLastMeasurementsAsyncEnumerable(
		int numberOfMeasurements);

	IAsyncEnumerable<(TraceLogDto?, Measurement?)> GetLastHistoryAsyncEnumerable(
		int numberOfMeasurements);
}