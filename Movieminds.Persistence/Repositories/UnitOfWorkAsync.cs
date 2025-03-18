using Microsoft.EntityFrameworkCore;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Persistence.Repositories;

public class UnitOfWorkAsync : UnitOfWork, IUnitOfWorkAsync
{
	public UnitOfWorkAsync(IFactory factory, DbContext context) : base(factory, context)
	{
	}

	public IRepositoryAsync<T> GetRepositoryAsync<T>() where T : BaseEntity
	{
		return GetRepository<T>() as IRepositoryAsync<T> ?? NullRepositoryAsync<T>.Instance;
	}

	public async Task SaveChangesAsync()
	{
		await Context.SaveChangesAsync();
	}

	protected override IRepository<T> CreateRepository<T>()
	{
		return new RepositoryAsync<T>(Factory, Context);
	}
}
