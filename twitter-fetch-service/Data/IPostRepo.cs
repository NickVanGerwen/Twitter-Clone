using twitter_fetch_service.Models;

namespace twitter_fetch_service.Data
{
    public interface IPostRepo
    {
        void CreatePost(Post post);
        IEnumerable<Post> GetAllPosts();
        Post GetPostById(int id);

        bool SaveChanges();
    }
}
