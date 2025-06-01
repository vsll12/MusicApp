using FavoritesService.Models;

namespace FavoritesService.Services
{
	public interface IFavoritesService
	{
		Task<IEnumerable<Favorite>> GetFavoritesByUserAsync(string userId);
		Task<Favorite?> GetFavoriteAsync(int id);
		Task<Favorite> AddFavoriteAsync(Favorite favorite);
		Task<bool> RemoveFavoriteAsync(int id);
	}
}
