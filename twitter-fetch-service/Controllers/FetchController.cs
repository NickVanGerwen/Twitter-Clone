using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using twitter_fetch_service.Data;
using twitter_fetch_service.DTOs;

namespace twitter_fetch_service.Controllers
{
    [Route("api/fetch")]
    [ApiController]
    public class FetchController : ControllerBase
    {
        readonly IPostRepo _repo;
        readonly IMapper _mapper;
        List<PostReadDTO> postReadDTOs;
        public FetchController(IPostRepo repo, IMapper mapper)
        {
            postReadDTOs = new List<PostReadDTO>()
            {
                new PostReadDTO()
                {
                    Id = 1,
                    Message = "woo a new post!",
                    Date = DateTime.Now,
                    Author = "John Bovi",
                    Likes = 16
                },               
                new PostReadDTO()
                {
                    Id = 1,
                    Message = "another post by john!",
                    Date = DateTime.Now,
                    Author = "John Bovi",
                    Likes = 3
                },                
                new PostReadDTO()
                {
                    Id = 1,
                    Message = "my opinion matters",
                    Date = DateTime.Now,
                    Author = "Frederik1983",
                    Likes = 376
                },

            };

            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostReadDTO>> GetPosts()
        {
            try
            {
                return Ok(postReadDTOs);
                //var postItems = _repo.GetAllPosts();
                //return Ok(_mapper.Map<IEnumerable<PostReadDTO>>(postItems));
            }
            catch (Exception ex)
            {
                Console.WriteLine("---------> " + ex.Message);
            }
            return Ok();
        }

        [HttpGet("{id}", Name = "GetPostById")]
        public ActionResult<PostReadDTO> GetPostById(int id)
        {
            var postItem = _repo.GetPostById(id);
            if (postItem != null)
            {
                return Ok(_mapper.Map<PostReadDTO>(postItem));
            }
            return NotFound();
        }
    }
}
