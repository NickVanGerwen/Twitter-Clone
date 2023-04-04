namespace twitter_post_service.DTOs
{
    public class PostReadDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public int Likes { get; set; }
    }
}
