using Microsoft.AspNetCore.Mvc;
using MusicService.Models;
using MusicService.Services;  

namespace MusicService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MusicPlaylistController : ControllerBase
	{
		private readonly IMusicPlaylistService _musicPlaylistService;

		public MusicPlaylistController(IMusicPlaylistService musicPlaylistService)
		{
			_musicPlaylistService = musicPlaylistService;
		}

		[HttpPost]
		public async Task<IActionResult> AddMusicToPlaylist([FromBody] MusicPlaylist musicPlaylist)
		{
			var added = await _musicPlaylistService.AddMusicToPlaylistAsync(musicPlaylist);
			if (!added)
			{
				return BadRequest("Failed to add music to playlist.");
			}
			return Ok("Music added to playlist successfully.");
		}

		[HttpDelete]
		public async Task<IActionResult> RemoveMusicFromPlaylist([FromQuery] int musicId, [FromQuery] int playlistId)
		{
			var removed = await _musicPlaylistService.RemoveMusicFromPlaylistAsync(musicId, playlistId);
			if (!removed)
			{
				return NotFound("Music or Playlist not found or association doesn't exist.");
			}
			return NoContent();
		}

		[HttpGet("{playlistId}")]
		public async Task<IActionResult> GetMusicByPlaylist(int playlistId)
		{
			var musicList = await _musicPlaylistService.GetMusicByPlaylistAsync(playlistId);
			if (musicList == null)
			{
				return NotFound("Playlist not found.");
			}
			return Ok(musicList);
		}
	}
}
