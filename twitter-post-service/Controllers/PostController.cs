﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using twitter_post_service.Data;
using twitter_post_service.DTOs;
using twitter_post_service.Models;
using twitter_post_service.RabbitMQ;

namespace twitter_post_service.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        readonly IPostRepo _repo;
        readonly IMapper _mapper;
        public PostController(IPostRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreatePost(PostCreateDTO postCreateDTO)
        {
            postCreateDTO.Date = DateTime.Now;
            RabbitmqPublisher rabbitmqPublisher = new RabbitmqPublisher();
            rabbitmqPublisher.Publish(postCreateDTO);
            var postModel = _mapper.Map<Post>(postCreateDTO);
            //_repo.CreatePost(postModel);
            //_repo.SaveChanges();
            return Ok();
        } 
    }
}
