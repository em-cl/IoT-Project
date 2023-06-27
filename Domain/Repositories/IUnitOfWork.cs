namespace Domain.Repositories;

public interface IUnitOfWork
{
    #region Data access code repositories
    ITraceLogRepo TraceLogs { get; }
    IMeasurementRepo Measurements { get; }
	#endregion
	int Save();
    Task<int> SaveAsync();
    void ChangeTrackerDetectChanges();
    void ClearChangeTracker();
    void Dispose();
}