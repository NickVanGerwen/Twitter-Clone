using twitter_post_service.DTOs;
using twitter_post_service.Models;

namespace twitter_post_service.Data
{
    public interface IPostRepo
    {
        void CreatePost(Post post);
        void DeletePost(Post post);
        bool SaveChanges();
        void UpdateUsername(AccountUpdateDto account);

        void DeleteUser(string Username);
    }
}
