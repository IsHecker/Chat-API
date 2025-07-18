using Microsoft.EntityFrameworkCore;

namespace Chat_API.Data.Repositories;

public abstract class Repository<TDomain, TRepository>
    where TDomain : class
    where TRepository : Repository<TDomain, TRepository>
{
    protected readonly ApplicationDbContext Context;
    protected IQueryable<TDomain> Query { get; set; }
    protected Repository(ApplicationDbContext context)
    {
        Context = context;
        Query = Context.Set<TDomain>();
    }

    public virtual Task<IQueryable<TDomain>> GetAllAsync()
    {
        return Task.FromResult(Query);
    }

    public virtual async Task<TDomain> AddAsync(TDomain entity)
    {
        return (await Context.AddAsync(entity)).Entity;
    }

    public async Task<IEnumerable<TDomain>> AddAsync(IEnumerable<TDomain> entities)
    {
        await Context.AddRangeAsync(entities);
        return entities;
    }

    public virtual Task UpdateAsync(TDomain entity)
    {
        Context.Update(entity);
        return Task.CompletedTask;
    }

    public TRepository AsTracking()
    {
        Query = Query.AsTracking();
        return (TRepository)this;
    }
}