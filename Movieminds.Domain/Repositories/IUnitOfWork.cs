using Movieminds.Domain.Entities;

namespace Movieminds.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
	IRepository<T> GetRepository<T>() where T : BaseEntity;

	void Begin();
	void Commit();
	void Rollback();

	void SaveChanges();
}
