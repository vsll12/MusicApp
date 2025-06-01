using Microsoft.EntityFrameworkCore;
using MusicService.Data;
using MusicService.Models;

namespace MusicService.Services
{
	public class MusicPlaylistService : IMusicPlaylistService
	{
		private readonly MusicDbContext _context; 

		public MusicPlaylistService(MusicDbContext context)
		{
			_context = context;
		}

		public async Task<bool> AddMusicToPlaylistAsync(MusicPlaylist musicPlaylist)
		{
			var exists = await _context.Set<MusicPlaylist>()
				.FindAsync(musicPlaylist.MusicId, musicPlaylist.PlaylistId);
			if (exists != null)
				return false;

			_context.Set<MusicPlaylist>().Add(musicPlaylist);
			var saved = await _context.SaveChangesAsync();
			return saved > 0;
		}

		public async Task<bool> RemoveMusicFromPlaylistAsync(int musicId, int playlistId)
		{
			var entity = await _context.Set<MusicPlaylist>()
				.FindAsync(musicId, playlistId);
			if (entity == null)
				return false;

			_context.Set<MusicPlaylist>().Remove(entity);
			var saved = await _context.SaveChangesAsync();
			return saved > 0;
		}

		public async Task<List<Music>> GetMusicByPlaylistAsync(int playlistId)
		{
			var playlist = await _context.Set<Playlist>()
				.FindAsync(playlistId);
			if (playlist == null)
				return null;

			var musicList = await _context.Set<MusicPlaylist>()
				.Where(mp => mp.PlaylistId == playlistId)
				.Select(mp => mp.Music)
				.ToListAsync();

			return musicList;
		}
	}
}
