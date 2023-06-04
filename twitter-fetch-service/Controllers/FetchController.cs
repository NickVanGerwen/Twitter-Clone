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
        public FetchController(IPostRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostReadDTO>> GetPosts()
        {
            try
            {
                var postItems = _repo.GetAllPosts();
                return Ok(_mapper.Map<IEnumerable<PostReadDTO>>(postItems));
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
