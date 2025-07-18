using Chat_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_API.Data.Repositories;

public abstract class EntityRepository<TEntity, TRepository> : Repository<TEntity, TRepository>
    where TEntity : Entity
    where TRepository : EntityRepository<TEntity, TRepository>
{
    protected EntityRepository(ApplicationDbContext context) : base(context)
    {
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await Query.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        return await Query.Where(e => e.Id == id).ExecuteDeleteAsync() > 0;
    }

    public virtual async Task<bool> DeleteAsync(TEntity entity)
    {
        return await DeleteAsync(entity.Id);
    }

    public async Task<bool> DeleteAsync(IEnumerable<Guid> entityIds)
    {
        return await Query.Where(e => entityIds.Contains(e.Id)).ExecuteDeleteAsync() > 0;
    }

    public async Task<bool> DeleteAsync(IEnumerable<TEntity> entities)
    {
        return await DeleteAsync(entities.Select(e => e.Id));
    }


    public async Task<bool> ExistsAsync(Guid id)
    {
        return await Query.AnyAsync(entity => entity.Id == id);
    }
}