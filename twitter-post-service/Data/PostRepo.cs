using Microsoft.EntityFrameworkCore;
using twitter_post_service.DTOs;
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

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        public void UpdateUsername(AccountUpdateDto account)
        {
            bool containsPostsOfAuthor = true;
            if (!string.IsNullOrEmpty(account.NewName))
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
            if (!string.IsNullOrEmpty(Username))
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
