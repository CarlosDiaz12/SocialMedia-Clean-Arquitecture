using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Core.Util;
using SocialMedia.Infrastrucuture.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public PostController(
            IPostService postRepository,
            IMapper mapper,
            IUriService uriService)
        {
            _postService = postRepository;
            _mapper = mapper;
            _uriService = uriService;
        }
        /// <summary>
        /// Retrieve all posts
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PostDto>>))]
        [HttpGet(Name = nameof(GetPosts))]
        public async Task<IActionResult> GetPosts([FromQuery] PostQueryFilter query)
        {
            var posts = await _postService.GetPosts(query);
            var postsDto = _mapper.Map<PagedResult<PostDto>>(posts);
            var metadata = _mapper.Map<Metadata>(postsDto);

            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto.Results, metadata);
            return Ok(response);
        }
        /// <summary>
        /// Get one post by Id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<PostDto>))]
        [HttpGet, Route("{postId}")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var post = await _postService.GetPostById(postId);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            if (postDto == null)
                return NotFound(response);

            return Ok(response);
        }
        /// <summary>
        /// Insert a post
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<PostDto>))]
        [HttpPost]
        public async Task<IActionResult> InsertPost(PostDto _object)
        {
            var newPost = _mapper.Map<Post>(_object);
            await _postService.InsertPost(newPost);
            var result = _mapper.Map<PostDto>(newPost);
            var response = new ApiResponse<PostDto>(result);
            return Ok(response);
        }
        /// <summary>
        /// Update a post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="_object"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody] PostDto _object)
        {
            var updatePost = _mapper.Map<Post>(_object);
            updatePost.Id = postId;
            await _postService.UpdatePost(updatePost);
            var response = new ApiResponse<bool>(true);
            return Ok(response);
        }
        /// <summary>
        /// Delete a post by Id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            await _postService.DeletePost(postId);
            var response = new ApiResponse<bool>(true);
            return Ok(response);
        }
    }
}
