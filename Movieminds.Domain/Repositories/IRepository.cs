using System.Linq.Expressions;
using Movieminds.Domain.Entities;

namespace Movieminds.Domain.Repositories;

public interface IRepository<T> where T : BaseEntity
{
	IQueryable<T> GetAll();

	T? GetById(params object?[]? id);

	T? Create(params object?[]? parameters);

	void Insert(T entity);
	void Update(T entity);
	void SoftDelete(T entity);
	void Delete(T entity);

	void BulkInsert(IEnumerable<T> entities);
	void BulkUpdate(IEnumerable<T> entities);
	void BulkSoftDelete(IEnumerable<T> entities);
	void BulkDelete(IEnumerable<T> entities);

	void Ensure<TProperty>(T entity, Expression<Func<T, TProperty>> expression) where TProperty : class;
	void Ensure<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> expression) where TProperty : class;
	void Ensure<TProperty>(T entity, Expression<Func<T, ICollection<TProperty>>> expression) where TProperty : class;
}
