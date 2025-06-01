using MusicService.Models;

namespace MusicService.Services
{
	public interface IMusicPlaylistService
	{
		Task<bool> AddMusicToPlaylistAsync(MusicPlaylist musicPlaylist);
		Task<bool> RemoveMusicFromPlaylistAsync(int musicId, int playlistId);
		Task<List<Music>> GetMusicByPlaylistAsync(int playlistId);

	}
}
