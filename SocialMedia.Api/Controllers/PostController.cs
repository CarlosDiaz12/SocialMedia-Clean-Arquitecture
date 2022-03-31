using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto);
            return Ok(response);
        }

        [HttpGet, Route("{postId}")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var post = await _postRepository.GetPostById(postId);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> InsertPost(PostDto _object)
        {
            var newPost = _mapper.Map<Post>(_object);
            await _postRepository.InsertPost(newPost);
            var result = _mapper.Map<PostDto>(newPost);
            var response = new ApiResponse<PostDto>(result);
            return Ok(response);
        }

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody] PostDto _object)
        {
            var updatePost = _mapper.Map<Post>(_object);
            updatePost.PostId = postId;
            var result = await _postRepository.UpdatePost(updatePost);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var result = await _postRepository.DeletePost(postId);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
