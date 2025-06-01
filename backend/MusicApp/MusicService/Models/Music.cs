namespace MusicService.Models
{
	public class Music
	{
		public int Id { get; set; }

		public string Title { get; set; } = null!;

		public string FilePath { get; set; } = null!; 

		public string UploadedByUserId { get; set; } = null!;

		public DateTime UploadedAt { get; set; }

		public string? PhotoUrl { get; set; } 
	}
}
