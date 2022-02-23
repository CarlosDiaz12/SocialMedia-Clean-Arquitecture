using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            var postsDto = posts.Select(x =>
                new PostDto
                {
                    PostId = x.PostId,
                    Date = x.Date,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    UserId = x.UserId
                });
            return Ok(postsDto);
        }

        [HttpGet, Route("{postId}")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var post = await _postRepository.GetPostById(postId);
            var postDto = new PostDto
            {
                PostId = post.PostId,
                Date = post.Date,
                Description = post.Description,
                ImageUrl = post.ImageUrl,
                UserId = post.UserId
            };
            return Ok(postDto);
        }
        [HttpPost]
        public async Task<IActionResult> InsertPost(PostDto _object)
        {
            var newPost = new Post
            {
                Date = _object.Date,
                Description = _object.Description,
                ImageUrl = _object.ImageUrl,
                UserId = _object.UserId
            };

             await _postRepository.InsertPost(newPost);
            return Ok(newPost);
        }
    }
}
