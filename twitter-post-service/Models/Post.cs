using System.ComponentModel.DataAnnotations;

namespace twitter_post_service.Models
{
    public class Post
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public int Likes { get; set; }
    }
}
