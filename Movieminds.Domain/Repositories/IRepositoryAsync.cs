using System.Linq.Expressions;
using Movieminds.Domain.Entities;

namespace Movieminds.Domain.Repositories;

public interface IRepositoryAsync<T> : IRepository<T> where T : BaseEntity
{
	Task<IEnumerable<T>> GetAllAsync();

	Task<T?> GetByIdAsync(params object?[]? id);

	Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

	Task InsertAsync(T entity);

	Task InsertBulkAsync(IEnumerable<T> entities);
}
