﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using twitter_post_service.Data;
using twitter_post_service.DTOs;
using twitter_post_service.Models;

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

        [HttpPost]
        public ActionResult<PostReadDTO> CreatePost(PostCreateDTO postCreateDTO)
        {
            var postModel = _mapper.Map<Post>(postCreateDTO);
            _repo.CreatePost(postModel);
            _repo.SaveChanges();
            var postReadDTO = _mapper.Map<PostReadDTO>(postModel);
            return CreatedAtRoute(nameof(GetPostById), new { Id = postReadDTO.Id }, postReadDTO);
        } 

    }
}