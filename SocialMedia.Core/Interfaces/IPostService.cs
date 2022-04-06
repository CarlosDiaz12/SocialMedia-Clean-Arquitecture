using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        Task<PagedResult<Post>> GetPosts(PostQueryFilter query);
        Task<Post> GetPostById(int Id);
        Task InsertPost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(int Id);
    }
}