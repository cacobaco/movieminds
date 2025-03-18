using Microsoft.EntityFrameworkCore;
using Movieminds.Domain.Entities;
using Movieminds.Persistence.Configurations;

namespace Movieminds.Persistence;

public class MoviemindsDbContext : DbContext
{
	public MoviemindsDbContext()
	{
	}

	public MoviemindsDbContext(DbContextOptions<MoviemindsDbContext> options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		if (optionsBuilder.IsConfigured)
		{
			return;
		}

		optionsBuilder.UseSqlite("Data Source=movieminds.db");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new UserConfiguration());
		modelBuilder.ApplyConfiguration(new ProfileConfiguration());
		modelBuilder.ApplyConfiguration(new MessageConfiguration());
		modelBuilder.ApplyConfiguration(new PostConfiguration());
		modelBuilder.ApplyConfiguration(new WishListConfiguration());
		modelBuilder.ApplyConfiguration(new SeenListConfiguration());
		modelBuilder.ApplyConfiguration(new GenreConfiguration());
		modelBuilder.ApplyConfiguration(new MovieConfiguration());
		modelBuilder.ApplyConfiguration(new MovieContributorConfiguration());
	}
}
