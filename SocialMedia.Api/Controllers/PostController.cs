using AutoMapper;
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

        [HttpGet(Name = nameof(GetPosts))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPosts([FromQuery] PostQueryFilter query)
        {
            var posts = await _postService.GetPosts(query);
            var postsDto = _mapper.Map<PagedResult<PostDto>>(posts);
            var metadata = _mapper.Map<Metadata>(postsDto);
            metadata.NextPageUrl = _uriService.GetPostPaginationUri(query, Url.RouteUrl(nameof(GetPosts))).ToString();
            metadata.PreviousPageUrl = _uriService.GetPostPaginationUri(query, Url.RouteUrl(nameof(GetPosts))).ToString();

            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto.Results, metadata);
            return Ok(response);
        }

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
        [HttpPost]
        public async Task<IActionResult> InsertPost(PostDto _object)
        {
            var newPost = _mapper.Map<Post>(_object);
            await _postService.InsertPost(newPost);
            var result = _mapper.Map<PostDto>(newPost);
            var response = new ApiResponse<PostDto>(result);
            return Ok(response);
        }

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody] PostDto _object)
        {
            var updatePost = _mapper.Map<Post>(_object);
            updatePost.Id = postId;
            await _postService.UpdatePost(updatePost);
            var response = new ApiResponse<bool>(true);
            return Ok(response);
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            await _postService.DeletePost(postId);
            var response = new ApiResponse<bool>(true);
            return Ok(response);
        }
    }
}
