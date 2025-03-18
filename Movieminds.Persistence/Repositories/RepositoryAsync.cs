using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Persistence.Repositories;

public class RepositoryAsync<T> : Repository<T>, IRepositoryAsync<T> where T : BaseEntity
{
	public RepositoryAsync(IFactory factory, DbContext context) : base(factory, context)
	{
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await Context.Set<T>().ToListAsync();
	}

	public async Task<T?> GetByIdAsync(params object?[]? id)
	{
		return await Context.FindAsync<T>(id);
	}

	public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
	{
		return await Context.Set<T>().FirstOrDefaultAsync(predicate);
	}

	public async Task InsertAsync(T entity)
	{
		await Context.AddAsync(entity);
	}

	public async Task InsertBulkAsync(IEnumerable<T> entities)
	{
		await Context.AddRangeAsync(entities);
	}
}
