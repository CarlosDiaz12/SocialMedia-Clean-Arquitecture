using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
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
                throw new BusinessException("User is not valid.");

            if (post.Description.ToUpper().Contains("SEXO"))
                throw new BusinessException("Post description is not allowed.");

            var userPosts = await _unitOfWork.PostRepository.GetPostsByUserId(post.UserId);

            if(userPosts.Count() > 0 && userPosts.Count() < 10)
            {
                var lastPost = userPosts.OrderBy(x => x.Date).Last();
                if((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("User can not publish.");
                }
            }

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
