using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		protected IFactory Factory { get; set; }
		protected DbContext Context { get; set; }
		private IDictionary<Type, object> Repositories { get; } = new Dictionary<Type, object>();
		private IDbContextTransaction? Transaction { get; set; }

		public UnitOfWork(IFactory factory, DbContext context)
		{
			Factory = factory;
			Context = context;
		}

		public IRepository<T> GetRepository<T>() where T : BaseEntity
		{
			var type = typeof(T);

			if (!Repositories.TryGetValue(type, out object? value))
			{
				value = CreateRepository<T>();
				Repositories[type] = value;
			}

			return (IRepository<T>)value;
		}

		public void Begin()
		{
			Transaction = Context.Database.BeginTransaction();
		}

		public void Commit()
		{
			Transaction?.Commit();
			Transaction = null;
		}

		public void Rollback()
		{
			Transaction?.Rollback();
			Transaction = null;
		}

		public void SaveChanges()
		{
			Context.SaveChanges();
		}

		public void Dispose()
		{
			Transaction?.Dispose();
			Context.Dispose();
		}

		protected virtual IRepository<T> CreateRepository<T>() where T : BaseEntity
		{
			return new Repository<T>(Factory, Context);
		}
	}
}
