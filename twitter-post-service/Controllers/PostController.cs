using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography.Xml;
using twitter_post_service.Data;
using twitter_post_service.DTOs;
using twitter_post_service.Models;
using twitter_post_service.Rabbitmq;

namespace twitter_post_service.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        readonly IPostRepo _repo;
        readonly IMapper _mapper;

        public PostController(IPostRepo repo, IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreatePost(PostCreateDTO postCreateDTO)
        {
            try
            {
                postCreateDTO.Date = DateTime.Now;
                RabbitmqPublisher rabbitmqPublisher = new RabbitmqPublisher();
                rabbitmqPublisher.Publish(postCreateDTO);
                var postModel = _mapper.Map<Post>(postCreateDTO);
                _repo.CreatePost(postModel);
                _repo.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return Ok();
        }
    }
}
