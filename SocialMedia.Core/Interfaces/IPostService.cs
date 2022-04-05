using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPostById(int Id);
        Task InsertPost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(int Id);
    }
}