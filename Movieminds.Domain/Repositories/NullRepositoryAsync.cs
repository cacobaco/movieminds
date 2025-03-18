using System.Linq.Expressions;
using Movieminds.Domain.Entities;

namespace Movieminds.Domain.Repositories;

public sealed class NullRepositoryAsync<T> : IRepositoryAsync<T> where T : BaseEntity
{
	public static NullRepositoryAsync<T> Instance { get; } = new NullRepositoryAsync<T>();

	private NullRepositoryAsync()
	{
	}

	public IQueryable<T> GetAll()
	{
		return Enumerable.Empty<T>().AsQueryable();
	}

	public T? GetById(params object?[]? id)
	{
		return null;
	}

	public T? Create(params object?[]? parameters)
	{
		return null;
	}

	public void Insert(T entity)
	{
	}

	public void SoftDelete(T entity)
	{
	}

	public void Update(T entity)
	{
	}

	public void Delete(T entity)
	{
	}

	public void BulkInsert(IEnumerable<T> entities)
	{
	}

	public void BulkUpdate(IEnumerable<T> entities)
	{
	}

	public void BulkSoftDelete(IEnumerable<T> entities)
	{
	}

	public void BulkDelete(IEnumerable<T> entities)
	{
	}

	public void Ensure<TProperty>(T entity, Expression<Func<T, TProperty>> expression) where TProperty : class
	{
	}

	public void Ensure<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> expression) where TProperty : class
	{
	}

	public void Ensure<TProperty>(T entity, Expression<Func<T, ICollection<TProperty>>> expression) where TProperty : class
	{
	}

	public Task<IEnumerable<T>> GetAllAsync()
	{
		return Task.FromResult(Enumerable.Empty<T>());
	}

	public Task<T?> GetByIdAsync(params object?[]? id)
	{
		return Task.FromResult<T?>(null);
	}

	public Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
	{
		return Task.FromResult<T?>(null);
	}

	public Task InsertAsync(T entity)
	{
		return Task.CompletedTask;
	}

	public Task InsertBulkAsync(IEnumerable<T> entities)
	{
		return Task.CompletedTask;
	}
}
