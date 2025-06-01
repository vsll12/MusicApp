using FavoritesService.Models;
using FavoritesService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FavoritesService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FavoritesController : ControllerBase
	{
		private readonly IFavoritesService _favoritesService;

		public FavoritesController(IFavoritesService favoritesService)
		{
			_favoritesService = favoritesService;
		}

		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetByUser(string userId)
		{
			var favorites = await _favoritesService.GetFavoritesByUserAsync(userId);
			return Ok(favorites);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var favorite = await _favoritesService.GetFavoriteAsync(id);
			if (favorite == null) return NotFound();
			return Ok(favorite);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Favorite favorite)
		{
			var added = await _favoritesService.AddFavoriteAsync(favorite);
			return CreatedAtAction(nameof(Get), new { id = added.Id }, added);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var removed = await _favoritesService.RemoveFavoriteAsync(id);
			if (!removed) return NotFound();
			return NoContent();
		}
	}
}
