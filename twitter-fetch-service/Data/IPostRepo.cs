using twitter_post_service.Models;

namespace twitter_post_service.Data
{
    public interface IPostRepo
    {
        IEnumerable<Post> GetAllPosts();
        Post GetPostById(int id);
        bool SaveChanges();
    }
}
