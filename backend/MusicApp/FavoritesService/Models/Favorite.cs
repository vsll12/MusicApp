namespace FavoritesService.Models
{
	public class Favorite
	{
		public int Id { get; set; }
		public string UserId { get; set; } = null!; 
		public int? MusicId { get; set; } 
		public int? PlaylistId { get; set; } 
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
