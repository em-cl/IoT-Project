using System.Diagnostics;
using Domain.Error;

namespace Domain.Shared
{

    public class ErrorCatcherService<TEntity> : IErrorCatcherService<TEntity> where TEntity : class
    {
        private const string Data = "Data";
        public ErrorCatcherService()
        {
             // bara output listener
            var sourceSwitch = new SourceSwitch(Data + "-switch")
            {
                Level = SourceLevels.All
            };
            //TraceSources.Instance[Data].Switch = sourceSwitch;
        }
        
        #region TryCatch wrappers no parameters
        public void Execute(Action @delegate)
        {
            try
            {
                if (@delegate is null) throw new ArgumentException(nameof(@delegate));
                @delegate.Invoke();
            }
            catch (Exception ex)
            {
                LoggException(ex, nameof(@delegate));
                throw;
            }
        }

        public async Task ExecuteAsync(Func<Task> @delegate)
        {
            try
            {
                if (@delegate is null) throw new ArgumentException(nameof(@delegate));
                await @delegate.Invoke();
            }
            catch (Exception ex)
            {
                LoggException(ex, nameof(@delegate));
                throw;
            }
        }

        public TEntity Execute(Func<TEntity> @delegate)
        {
            try
            {
                if (@delegate is null) throw new ArgumentException(nameof(@delegate));
                return @delegate.Invoke();
            }
            catch (Exception ex)
            {
                LoggException(ex, nameof(@delegate));
                throw;
            }
        }
        public async Task<TEntity> ExecuteAsync(Func<Task<TEntity>> @delegate)
        {
            try
            {
                if (@delegate is null) throw new ArgumentException(nameof(@delegate));
                return await @delegate();
            }
            catch (Exception ex)
            {
                LoggException(ex, nameof(@delegate));
                throw;
            }
        }

        public IEnumerable<TEntity> Execute(Func<IEnumerable<TEntity>> @delegate)
        {
            try
            {
                if (@delegate is null) throw new ArgumentException(nameof(@delegate));
                return @delegate.Invoke();
            }
            catch (Exception ex)
            {
                LoggException(ex, nameof(@delegate));
                throw;
            }
        }
        // using func async method source: Anders Andreen ISP Handledning
        public async Task<IEnumerable<TEntity>> ExecuteAsync(Func<Task<IEnumerable<TEntity>>> @delegate)
        {
            try
            {
                if (@delegate is null) throw new ArgumentException(nameof(@delegate));
                return await @delegate();
            }
            catch (Exception ex)
            {
                LoggException(ex, nameof(@delegate));
                throw;
            }
        }
        #endregion

        private static void LoggException(Exception ex, string @delegate)
        {
            Debug.WriteLine(@delegate+ " in "+ ex.GetAllMessages().ExtraDebugInfo());
        }

    }
}
