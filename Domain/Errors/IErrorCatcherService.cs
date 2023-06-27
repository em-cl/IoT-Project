namespace Domain.Shared
{
    public interface IErrorCatcherService<TEntity> where TEntity : class?
    {
        void Execute(Action @delegate);
        TEntity Execute(Func<TEntity> @delegate);
        IEnumerable<TEntity> Execute(Func<IEnumerable<TEntity>> @delegate);
        Task ExecuteAsync(Func<Task> @delegate);
        Task<TEntity> ExecuteAsync(Func<Task<TEntity>> @delegate);
        Task<IEnumerable<TEntity>> ExecuteAsync(Func<Task<IEnumerable<TEntity>>> @delegate);
    }
}