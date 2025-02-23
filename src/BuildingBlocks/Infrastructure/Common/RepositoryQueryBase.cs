using System.Linq.Expressions;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public class RepositoryQueryBase<T, K, TContext> : IRepositoryQueryBase<T, K, TContext>
    where T : EntityBase<K>
    where TContext : DbContext
{
    private readonly TContext _context;

    public RepositoryQueryBase(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public IQueryable<T> FindAll(bool trackingChanges = false) =>
        !trackingChanges ? _context.Set<T>().AsNoTracking() :
            _context.Set<T>();

    public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeExpressions)
    {
        var items = FindAll(trackChanges);
        items = includeExpressions.Aggregate(items, (current, includeExpression) => current.Include(includeExpression));
        return items;
    }
    
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackingChanges = false) =>
        !trackingChanges
            ? _context.Set<T>().Where(expression).AsNoTracking()
            : _context.Set<T>().Where(expression);

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackingChanges = false,
        params Expression<Func<T, object>>[] includeExpressions)
    {
        var items = FindByCondition(expression, trackingChanges);
        items = includeExpressions.Aggregate(items, (current, includeExpression) => current.Include(includeExpression));
        return items;
    }

    public async Task<T?> GetByIdAsync(K id) =>
        await FindByCondition(x => x.Id.Equals(id))
            .FirstOrDefaultAsync();
    
    public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeExpressions) =>
        await FindByCondition(x => x.Id.Equals(id), false, includeExpressions)
            .FirstOrDefaultAsync();
}