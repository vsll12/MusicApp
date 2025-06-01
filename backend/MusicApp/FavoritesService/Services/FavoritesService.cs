using FavoritesService.Data;
using FavoritesService.Models;
using Microsoft.EntityFrameworkCore;

namespace FavoritesService.Services
{
	public class FavoritesService : IFavoritesService
	{
		private readonly FavoritesDbContext _context;

		public FavoritesService(FavoritesDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Favorite>> GetFavoritesByUserAsync(string userId)
		{
			return await _context.Favorites
				.Where(f => f.UserId == userId)
				.ToListAsync();
		}

		public async Task<Favorite?> GetFavoriteAsync(int id)
		{
			return await _context.Favorites.FindAsync(id);
		}

		public async Task<Favorite> AddFavoriteAsync(Favorite favorite)
		{
			_context.Favorites.Add(favorite);
			await _context.SaveChangesAsync();
			return favorite;
		}

		public async Task<bool> RemoveFavoriteAsync(int id)
		{
			var favorite = await _context.Favorites.FindAsync(id);
			if (favorite == null) return false;

			_context.Favorites.Remove(favorite);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
