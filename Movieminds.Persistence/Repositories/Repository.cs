using Microsoft.EntityFrameworkCore;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;
using System.Linq.Expressions;

namespace Movieminds.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
	protected IFactory Factory { get; set; }
	protected DbContext Context { get; set; }

	public Repository(IFactory factory, DbContext context)
	{
		Factory = factory;
		Context = context;
	}

	public IQueryable<T> GetAll()
	{
		return Context.Set<T>();
	}

	public T? GetById(params object?[]? id)
	{
		return Context.Find<T>(id);
	}

	public void Insert(T entity)
	{
		Context.Add(entity);
	}

	public void Update(T entity)
	{
		entity.Update();
		Context.Update(entity);
	}

	public void SoftDelete(T entity)
	{
		entity.Delete();
		Context.Update(entity);
	}

	public void Delete(T entity)
	{
		Context.Remove(entity);
	}

	public void BulkInsert(IEnumerable<T> entities)
	{
		Context.AddRange(entities);
	}

	public void BulkUpdate(IEnumerable<T> entities)
	{
		foreach (var entity in entities)
		{
			entity.Update();
		}
		Context.UpdateRange(entities);
	}

	public void BulkSoftDelete(IEnumerable<T> entities)
	{
		foreach (var entity in entities)
		{
			entity.Delete();
		}
		Context.UpdateRange(entities);
	}

	public void BulkDelete(IEnumerable<T> entities)
	{
		Context.RemoveRange(entities);
	}

	public T? Create(params object?[]? parameters)
	{
		return Factory.Create<T>(parameters);
	}

	public void Ensure<TProperty>(T entity, Expression<Func<T, TProperty>> expression) where TProperty : class
	{
		Context.Entry(entity).Reference(expression).Load();
	}

	public void Ensure<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> expression) where TProperty : class
	{
		Context.Entry(entity).Collection(expression).Load();
	}

	public void Ensure<TProperty>(T entity, Expression<Func<T, ICollection<TProperty>>> expression) where TProperty : class
	{
		var parameter = expression.Parameters[0];
		var body = Expression.Convert(expression.Body, typeof(IEnumerable<TProperty>));
		var lambda = Expression.Lambda<Func<T, IEnumerable<TProperty>>>(body, parameter);

		Context.Entry(entity).Collection(lambda).Load();
	}
}
