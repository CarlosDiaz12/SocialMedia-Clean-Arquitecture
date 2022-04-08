using Microsoft.Extensions.Options;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = paginationOptions.Value;
        }
        public Task<Post> GetPostById(int Id)
        {
            return _unitOfWork.PostRepository.GetById(Id);
        }

        public async Task<PagedResult<Post>> GetPosts(PostQueryFilter query)
        {
            query.PageNumber = (query.PageNumber == 0 || query.PageNumber < 0) ? _paginationOptions.DefaultPageNumber : query.PageNumber;
            query.PageSize = (query.PageSize == 0 || query.PageSize < 0) ? _paginationOptions.DefaultPageSize : query.PageSize;

            var predicate = PredicateBuilder.True<Post>();

            if (query.UserId.HasValue)
                predicate = predicate.And(x => x.UserId == query.UserId);

            if (query.Date.HasValue)
                predicate = predicate.And(x => x.Date.Date == query.Date.Value.Date);

            if (!string.IsNullOrWhiteSpace(query.Description))
                predicate = predicate.And(x => x.Description.Contains(query.Description));

            var posts = await _unitOfWork.PostRepository.GetAll(predicate);
            return Pagination<Post>.GetPagedResultForQuery(posts, query.PageNumber, query.PageSize, true);
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
