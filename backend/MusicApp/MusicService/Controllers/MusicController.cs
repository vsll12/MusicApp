using Microsoft.AspNetCore.Mvc;
using MusicService.Models;
using MusicService.Services;

namespace MusicService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MusicController : ControllerBase
	{
		private readonly IMusicService _musicService;

		public MusicController(IMusicService musicService)
		{
			_musicService = musicService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var music = await _musicService.GetAllAsync();
			return Ok(music);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var music = await _musicService.GetByIdAsync(id);
			return music == null ? NotFound() : Ok(music);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Music music)
		{
			var result = await _musicService.CreateAsync(music);
			return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _musicService.DeleteAsync(id);
			return deleted ? NoContent() : NotFound();
		}
	}
}
