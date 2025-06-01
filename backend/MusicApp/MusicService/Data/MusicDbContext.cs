using Microsoft.EntityFrameworkCore;
using MusicService.Models;

namespace MusicService.Data
{
	public class MusicDbContext : DbContext
	{
		public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options) { }

		public DbSet<Music> Musics { get; set; }
		public DbSet<Playlist> Playlists { get; set; }
		public DbSet<MusicPlaylist> MusicPlaylists { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MusicPlaylist>()
				.HasKey(mp => new { mp.MusicId, mp.PlaylistId });

			modelBuilder.Entity<MusicPlaylist>()
				.HasOne(mp => mp.Music)
				.WithMany()
				.HasForeignKey(mp => mp.MusicId);

			modelBuilder.Entity<MusicPlaylist>()
				.HasOne(mp => mp.Playlist)
				.WithMany(p => p.MusicPlaylists)
				.HasForeignKey(mp => mp.PlaylistId);
		}
	}
}
