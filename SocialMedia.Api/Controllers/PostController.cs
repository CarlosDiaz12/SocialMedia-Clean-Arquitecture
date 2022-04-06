using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        public PostController(IPostService postRepository, IMapper mapper)
        {
            _postService = postRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ApiResponse<IEnumerable<PostDto>>>> GetPosts([FromQuery] PostQueryFilter query)
        {
            var posts = await _postService.GetPosts(query);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto);
            return Ok(response);
        }

        [HttpGet, Route("{postId}")]
        public async Task<ActionResult<ApiResponse<PostDto>>> GetPostById(int postId)
        {
            var post = await _postService.GetPostById(postId);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            if (postDto == null)
                return NotFound(response);

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PostDto>>> InsertPost(PostDto _object)
        {
            var newPost = _mapper.Map<Post>(_object);
            await _postService.InsertPost(newPost);
            var result = _mapper.Map<PostDto>(newPost);
            var response = new ApiResponse<PostDto>(result);
            return Ok(response);
        }

        [HttpPut("{postId}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdatePost(int postId, [FromBody] PostDto _object)
        {
            var updatePost = _mapper.Map<Post>(_object);
            updatePost.Id = postId;
            await _postService.UpdatePost(updatePost);
            var response = new ApiResponse<bool>(true);
            return Ok(response);
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePost(int postId)
        {
            await _postService.DeletePost(postId);
            var response = new ApiResponse<bool>(true);
            return Ok(response);
        }
    }
}
