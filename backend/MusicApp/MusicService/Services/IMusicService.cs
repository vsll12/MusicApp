using MusicService.Models;

namespace MusicService.Services
{
	public interface IMusicService
	{
		Task<List<Music>> GetAllAsync();
		Task<Music?> GetByIdAsync(int id);
		Task<Music> CreateAsync(Music music);
		Task<bool> DeleteAsync(int id);
	}
}
