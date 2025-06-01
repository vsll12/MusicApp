using Microsoft.EntityFrameworkCore;
using FavoritesService.Models;

namespace FavoritesService.Data
{
	public class FavoritesDbContext : DbContext
	{
		public FavoritesDbContext(DbContextOptions<FavoritesDbContext> options)
			: base(options)
		{
		}

		public DbSet<Favorite> Favorites => Set<Favorite>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Favorite>()
				.HasKey(f => f.Id);

			modelBuilder.Entity<Favorite>()
				.HasIndex(f => new { f.UserId, f.MusicId, f.PlaylistId })
				.IsUnique(); 
		}
	}
}
