using Movieminds.Domain.Entities;

namespace Movieminds.Domain.Repositories;

public interface IUnitOfWorkAsync : IUnitOfWork
{
	IRepositoryAsync<T> GetRepositoryAsync<T>() where T : BaseEntity;

	Task SaveChangesAsync();
}
