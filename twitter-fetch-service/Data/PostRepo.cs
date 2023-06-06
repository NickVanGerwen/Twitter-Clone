using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using twitter_fetch_service.DTOs;
using twitter_fetch_service.Models;

namespace twitter_fetch_service.Data
{
    public class PostRepo : IPostRepo
    {
        private readonly AppDbContext _context;

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
        public PostRepo(AppDbContext context)
        {
            _context = context;
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

        public void UpdateUsername(AccountUpdateDto account)
        {
            bool containsPostsOfAuthor = true;
            if (!account.NewName.IsNullOrEmpty())
            {
                while (containsPostsOfAuthor)
                {
                    if (!_context.Posts.Any(p => p.Author == account.OldName))
                    {
                        containsPostsOfAuthor = false;
                    }
                    else
                    {
                        _context.Posts.First(p => p.Author == account.OldName).Author = account.NewName;
                        _context.SaveChanges();
                    }
                }
            }
        }

        public void DeleteUser(string Username)
        {
            bool containsPostsOfAuthor = true;
            if (!Username.IsNullOrEmpty())
            {
                while (containsPostsOfAuthor)
                {
                    if (!_context.Posts.Any(p => p.Author == Username))
                    {
                        containsPostsOfAuthor = false;
                    }
                    else
                    {
                        _context.Posts.First(p => p.Author == Username).Author = "[Deleted]";
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}
