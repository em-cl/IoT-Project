using Domain.Repositories;

namespace Presistence.Repositories
{
    //Repository pattern and unit of work pattern generic example: https://www.youtube.com/watch?v=rtXpYpZdOzM

    public class UnitOfWork : IUnitOfWork
    {
        private readonly CleanDbContext _dbContext;
        
        public IMeasurementRepo Measurements { get; }
        public ITraceLogRepo TraceLogs { get; }

        public UnitOfWork(CleanDbContext dbContext)
        {
            _dbContext = dbContext;
            TraceLogs = new TraceLogRepo(_dbContext);
            Measurements = new MeasurementRepo(_dbContext);
        }

        public int Save() => _dbContext.SaveChanges();
        public Task<int> SaveAsync() => _dbContext.SaveChangesAsync();
        public void ChangeTrackerDetectChanges() => _dbContext.ChangeTracker.DetectChanges();
        public void ClearChangeTracker() => _dbContext.ChangeTracker.Clear();
        //GC.suppressFinalize Source: https://www.c-sharpcorner.com/article/implementing-unit-of-work-and-repository-pattern-with-dependency-injection-in-n/
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
