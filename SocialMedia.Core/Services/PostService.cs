using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public Task<bool> DeletePost(int Id)
        {
            return _postRepository.DeletePost(Id);
        }

        public Task<Post> GetPostById(int Id)
        {
            return _postRepository.GetPostById(Id);
        }

        public Task<IEnumerable<Post>> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _userRepository.GetUserById(post.UserId);
            if (user == null)
                throw new Exception("User is not valid.");

            if (post.Description.ToUpper().Contains("SEXO"))
                throw new Exception("Post description is not allowed.");

            await _postRepository.InsertPost(post);
        }

        public Task<bool> UpdatePost(Post post)
        {
            return _postRepository.UpdatePost(post);
        }
    }
}
