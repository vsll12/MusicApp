using Microsoft.EntityFrameworkCore;
using MusicService.Data;
using MusicService.Models;

namespace MusicService.Services
{
	public class MusicService : IMusicService
	{
		private readonly MusicDbContext _context;

		public MusicService(MusicDbContext context)
		{
			_context = context;
		}

		public async Task<List<Music>> GetAllAsync()
		{
			return await _context.Musics.ToListAsync();
		}

		public async Task<Music?> GetByIdAsync(int id)
		{
			return await _context.Musics.FindAsync(id);
		}

		public async Task<Music> CreateAsync(Music music)
		{
			_context.Musics.Add(music);
			await _context.SaveChangesAsync();
			return music;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var music = await _context.Musics.FindAsync(id);
			if (music == null) return false;

			_context.Musics.Remove(music);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
