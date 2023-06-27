using System.Linq.Expressions;

namespace Domain.Repositories;

public interface IBaseRepository<TEntity, in TKey> where TEntity : class
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    TEntity Get(TKey id);
    TEntity Get(Expression<Func<TEntity, bool>> filter);
    TEntity GetMarkedForEdit(TKey id);
    Task<TEntity> GetAsync(TKey id);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
    TEntity GetAsNoTracking(TKey id);
    TEntity GetAsNoTracking(Expression<Func<TEntity, bool>> filter);
    Task<TEntity> GetAsNoTrackingAsync(TKey id);
    Task<TEntity> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);

    IEnumerable<TEntity> GetAll(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

    IEnumerable<TEntity> GetAll(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);

    Task<IEnumerable<TEntity>> GetAllAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

    IEnumerable<TEntity> GetAllAsNoTracking();
    IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, bool>> filter);

    IEnumerable<TEntity> GetAllAsNoTracking(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

    IEnumerable<TEntity> GetAllAsNoTracking(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

    Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync();
    Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter);

    Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

    Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

    IAsyncEnumerable<TEntity> GetAllAsyncEnumerable();
    IAsyncEnumerable<TEntity> GetAllAsyncEnumerable(Expression<Func<TEntity, bool>> filter);
    IAsyncEnumerable<TEntity> GetAllAsNoTrackingAsyncEnumerable();

    IAsyncEnumerable<TEntity> GetAllAsNoTrackingAsyncEnumerable(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

    IAsyncEnumerable<TEntity> GetAllAsNoTrackingAsyncEnumerable(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void Remove(TKey id);
    void RemoveRange(IEnumerable<TEntity> entities);
}