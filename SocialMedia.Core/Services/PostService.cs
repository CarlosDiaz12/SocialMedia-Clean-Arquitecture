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
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Post> GetPostById(int Id)
        {
            return _unitOfWork.PostRepository.GetById(Id);
        }

        public Task<IEnumerable<Post>> GetPosts()
        {
            return _unitOfWork.PostRepository.GetAll();
        }
        public async Task DeletePost(int Id)
        {
            await _unitOfWork.PostRepository.Delete(Id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if (user == null)
                throw new Exception("User is not valid.");

            if (post.Description.ToUpper().Contains("SEXO"))
                throw new Exception("Post description is not allowed.");

            var userPosts = _unitOfWork.PostRepository.get

            await _unitOfWork.PostRepository.Insert(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePost(Post post)
        {
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
