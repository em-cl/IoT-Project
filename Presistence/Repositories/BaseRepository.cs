using System.Linq.Expressions;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Repositories;

public class BaseRepository<TEntity,TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
{
    protected readonly CleanDbContext Context;
    protected readonly IErrorCatcherService<TEntity> Ecs;
    public BaseRepository(CleanDbContext context)
    {
        Context = context;
        Ecs = new ErrorCatcherService<TEntity>();
    }
    #region Create
    public void Add(TEntity entity) 
        => Ecs.Execute(() 
            => Context.Set<TEntity>().Add(entity));
    //Execute action alternative source: https://stackoverflow.com/questions/33941583/converting-action-method-call-to-async-action-method-call
    public async Task AddAsync(TEntity entity) 
        => await Ecs.ExecuteAsync(async () 
            => await Context.Set<TEntity>().AddAsync(entity));
    public void AddRange(IEnumerable<TEntity> entities) 
        => Ecs.Execute(() 
            => Context.AddRange(entities));
    public async Task AddRangeAsync(IEnumerable<TEntity> entities) 
        => await Ecs.ExecuteAsync(async () 
            => await Context.AddRangeAsync(entities));
    #endregion

    #region Read
    //==================================================================== OneRow

    #region Get Overloads
    public TEntity Get(TKey id) 
        => Ecs.Execute(() 
            => (Context.Set<TEntity>().Find(id) ?? null)!);
    public TEntity GetMarkedForEdit(TKey id) =>
        Ecs.Execute(() =>
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity is null) return null!;
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        });
    public TEntity Get(Expression<Func<TEntity, bool>> filter) 
        => Ecs.Execute(() 
            => Context.Set<TEntity>().Where(filter).FirstOrDefault()!);
    #endregion

    #region GetAsync Overloads
    public async Task<TEntity>GetAsync(TKey id) 
        => await Ecs.ExecuteAsync( async () 
            => (await Context.Set<TEntity>().FindAsync(id))!);
    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        => await Ecs.ExecuteAsync(async () 
            => (await Context.Set<TEntity>().Where(filter).FirstOrDefaultAsync())!);
    #endregion

    #region GetAsNoTracking Overloads
    //remove tracking from find: https://stackoverflow.com/questions/34967116/how-to-combine-find-and-asnotracking
    public TEntity GetAsNoTracking(TKey id) =>
        Ecs.Execute(() =>
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity is null) return null!;
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        });

    public TEntity GetAsNoTracking(Expression<Func<TEntity, bool>> filter) =>
        Ecs.Execute(() =>
        {
            var entity = Context.Set<TEntity>().Where(filter).FirstOrDefault();
            if (entity is null) return null!;
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        });

    #endregion

    #region GetAsNoTrackingAsync overloads
    public async Task<TEntity> GetAsNoTrackingAsync(TKey id) =>
        await Ecs.ExecuteAsync(async () =>
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity is null) return null;
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        });

    public async Task<TEntity> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter) =>
        await Ecs.ExecuteAsync(async () =>
        {
            var entity = await Context.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
            if (entity is null) return null;
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        });
    #endregion
    //==================================================================== MultipleRows

    #region GetAll overloads
    public IEnumerable<TEntity> GetAll() => Ecs.Execute(() => Context.Set<TEntity>().ToList());
    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter) => Ecs.Execute(() => Context.Set<TEntity>().Where(filter).ToList());
    public IEnumerable<TEntity> GetAll(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        => Ecs.Execute(() => orderBy(Context.Set<TEntity>()).ToList());
    public IEnumerable<TEntity> GetAll(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        => Ecs.Execute(() => orderBy(Context.Set<TEntity>().Where(filter)).ToList());

    #endregion

    #region GetAllAsync overloads
    public async Task<IEnumerable<TEntity>> GetAllAsync() => await Ecs.ExecuteAsync(async () => await Context.Set<TEntity>().ToListAsync());
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        => await Ecs.ExecuteAsync(async () 
            => await Context.Set<TEntity>().Where(filter).ToListAsync());
    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        => await Ecs.ExecuteAsync(async () 
            => await orderBy(Context.Set<TEntity>()).ToListAsync());
    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        => await Ecs.ExecuteAsync(async () 
            => await orderBy(Context.Set<TEntity>().Where(filter)).ToListAsync());
    #endregion

    #region GetAllAsNoTracking overloads
    public IEnumerable<TEntity> GetAllAsNoTracking()
        => Ecs.Execute(() 
            => Context.Set<TEntity>().AsNoTracking().ToList());
    public IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, bool>> filter)
        => Ecs.Execute(() 
            => Context.Set<TEntity>().AsNoTracking().Where(filter).ToList());
    public IEnumerable<TEntity> GetAllAsNoTracking(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        => Ecs.Execute(() => orderBy(Context.Set<TEntity>().AsNoTracking()).ToList());
    public IEnumerable<TEntity> GetAllAsNoTracking(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        => Ecs.Execute(() => orderBy(Context.Set<TEntity>().AsNoTracking().Where(filter)).ToList());
    #endregion

    #region GetAllAsNoTrackingAsync Overloads
    public async Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync()
        => await Ecs.ExecuteAsync(async () 
            => await Context.Set<TEntity>().AsNoTracking().ToListAsync());
    public async Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter)
        => await Ecs.ExecuteAsync(async () 
            => await Context.Set<TEntity>().AsNoTracking().Where(filter).ToListAsync());
    public async Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        => await Ecs.ExecuteAsync(async () 
            => await orderBy(Context.Set<TEntity>().AsNoTracking()).ToListAsync());
    public async Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        => await Ecs.ExecuteAsync(async () 
            => await orderBy(Context.Set<TEntity>().AsNoTracking().Where(filter)).ToListAsync());
    #endregion

    #region GetAllAsyncEnumerable Overloads
    //TODO TryCatch is missing not used
    public async IAsyncEnumerable<TEntity> GetAllAsyncEnumerable()
    {
        await foreach (var entity in Context.Set<TEntity>().AsAsyncEnumerable())
        {
            yield return entity;
        }
    }
    //TODO TryCatch is missing not used
    public async IAsyncEnumerable<TEntity> GetAllAsyncEnumerable(Expression<Func<TEntity, bool>> filter)
    {
        await foreach (var entity in Context.Set<TEntity>().Where(filter).AsAsyncEnumerable())
        {
            yield return entity;
        }
    }
    #endregion

    #region GetAllAsNoTrackingAsyncEnumerable overloads
    //TODO TryCatch is missing not used
    public async IAsyncEnumerable<TEntity> GetAllAsNoTrackingAsyncEnumerable()
    {
        await foreach (var entity in Context.Set<TEntity>().AsNoTracking().AsAsyncEnumerable())
        {
            yield return entity;
        }
    }
    //TODO TryCatch is missing not used
    public async IAsyncEnumerable<TEntity> GetAllAsNoTrackingAsyncEnumerable(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
    {
        await foreach (var entity in orderBy(Context.Set<TEntity>().AsNoTracking()).AsAsyncEnumerable())
        {
            yield return entity;
        }
    }
    //TODO TryCatch is missing not used
    public async IAsyncEnumerable<TEntity> GetAllAsNoTrackingAsyncEnumerable(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
    {
        await foreach (var entity in orderBy(Context.Set<TEntity>().AsNoTracking().Where(filter)).AsAsyncEnumerable())
        {
            yield return entity;
        }
    }
    #endregion

    #endregion

    #region Update
    public void Update(TEntity entity)
    {
        Ecs.Execute(() => Context.Set<TEntity>().Update(entity));
        Context.SaveChanges();
        Context.ChangeTracker.Clear();
    }
    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        Ecs.Execute(() => Context.Set<TEntity>().UpdateRange(entities));
        Context.SaveChanges();
        Context.ChangeTracker.Clear();
    }
    #endregion

    #region Delete
    public void Remove(TEntity entity) 
        => Ecs.Execute(() 
            => Context.Set<TEntity>().Remove(entity));
    public void Remove(TKey id) =>
        Ecs.Execute(() =>
        {
            if (id is 0) return;
            var entity = Context.Set<TEntity>().Find(id);
            if (entity is null) return;
            Context.Set<TEntity>().Remove(entity);
        });
    public void RemoveRange(IEnumerable<TEntity> entities) 
        => Ecs.Execute(() 
            => Context.RemoveRange(entities));
    #endregion


}