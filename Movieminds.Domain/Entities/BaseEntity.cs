namespace Movieminds.Domain.Entities;

public abstract class BaseEntity
{
	public int Id { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public DateTime? UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }
	public bool IsDeleted => DeletedAt.HasValue;

	public void Update()
	{
		UpdatedAt = DateTime.Now;
	}

	public void Delete()
	{
		DeletedAt = DateTime.Now;
	}

	public void Restore()
	{
		DeletedAt = null;
	}
}
