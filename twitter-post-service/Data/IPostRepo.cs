using twitter_post_service.Models;

namespace twitter_post_service.Data
{
    public interface IPostRepo
    {
        IEnumerable<Post> GetAllPosts();
        Post GetPostById(int id);
        void CreatePost(Post post);
        void DeletePost(Post post);
        bool SaveChanges();
    }
}
