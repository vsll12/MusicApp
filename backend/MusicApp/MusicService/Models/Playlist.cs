namespace MusicService.Models
{
	public class Playlist
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string OwnerUserId { get; set; }
		public List<MusicPlaylist> MusicPlaylists { get; set; } = new();
	}
}
