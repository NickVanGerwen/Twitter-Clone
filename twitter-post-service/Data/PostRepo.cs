using Microsoft.EntityFrameworkCore;
using twitter_post_service.Models;

namespace twitter_post_service.Data
{
    public class PostRepo : IPostRepo
    {
        private readonly AppDbContext _context;

        public PostRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePost(Post post)
        {
            if (post != null)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
            }
            else
                throw new ArgumentNullException(nameof(post));

        }

        public void DeletePost(Post post)
        {
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
            else
                throw new ArgumentNullException(nameof(post));
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
