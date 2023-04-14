using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using twitter_post_service.Data;
using twitter_post_service.DTOs;

namespace twitter_fetch_service.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class FetchController : ControllerBase
    {
        readonly IPostRepo _repo;
        readonly IMapper _mapper;
        public FetchController(IPostRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostReadDTO>> GetPosts()
        {
            var postItems = _repo.GetAllPosts();
            return Ok(_mapper.Map<IEnumerable<PostReadDTO>>(postItems));
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
