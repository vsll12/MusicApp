using Microsoft.AspNetCore.Mvc;
using MusicService.Models;
using MusicService.Services;
using Microsoft.Extensions.FileProviders;

namespace MusicService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MusicController : ControllerBase
	{
		private readonly IMusicService _musicService;
		private readonly IWebHostEnvironment _env;

		public MusicController(IMusicService musicService, IWebHostEnvironment env)
		{
			_musicService = musicService;
			_env = env;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var musicList = await _musicService.GetAllAsync();
			var baseUrl = $"{Request.Scheme}://{Request.Host}/api/music/files/";

			foreach (var music in musicList)
			{
				music.FilePath = baseUrl + music.FilePath;
			}

			return Ok(musicList);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var music = await _musicService.GetByIdAsync(id);
			if (music == null)
				return NotFound();

			var baseUrl = $"{Request.Scheme}://{Request.Host}/api/music/files/";
			music.FilePath = baseUrl + music.FilePath;

			return Ok(music);
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

		[HttpPost("upload")]
		public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string title, [FromForm] string uploadedByUserId)
		{
			if (file == null || file.Length == 0)
				return BadRequest("No file uploaded.");

			var uploadsPath = Path.Combine(_env.ContentRootPath, "MusicFiles");
			Directory.CreateDirectory(uploadsPath);

			var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
			var filePath = Path.Combine(uploadsPath, uniqueFileName);

			try
			{
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				var music = new Music
				{
					Title = title,
					FilePath = uniqueFileName,
					UploadedByUserId = uploadedByUserId,
					UploadedAt = DateTime.UtcNow
				};

				var result = await _musicService.CreateAsync(music);

				var baseUrl = $"{Request.Scheme}://{Request.Host}/api/music/files/";
				result.FilePath = baseUrl + result.FilePath;

				return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"An error occurred while uploading the file: {ex.Message}");
			}
		}
	}
}
