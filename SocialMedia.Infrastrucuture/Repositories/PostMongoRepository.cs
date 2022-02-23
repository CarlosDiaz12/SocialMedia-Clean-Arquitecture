using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastrucuture.Repositories
{
    public class PostMongoRepository : IPostRepository
    {
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = Enumerable.Range(1, 10)
                .Select(x => new Post()
                {
                    PostId = x,
                    userId = x,
                    Description = $"Description {x} Mongo",
                    Date = DateTime.Now,
                    ImageUrl = $"https://misapis.com/{x}"
                });
            await Task.Delay(100);
            return posts;
        }
    }
}
