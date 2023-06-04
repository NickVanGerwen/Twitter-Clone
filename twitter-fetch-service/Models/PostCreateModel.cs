using System.ComponentModel.DataAnnotations;

namespace twitter_fetch_service.Models
{
    public class PostCreateModel
    {
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public int Likes { get; set; }
    }
}
