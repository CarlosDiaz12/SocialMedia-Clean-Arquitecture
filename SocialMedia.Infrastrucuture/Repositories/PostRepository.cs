using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialMedia.Infrastrucuture.Repositories
{
    public class PostRepository
    {
        public IEnumerable<Post> GetPosts()
        {
            var posts = Enumerable.Range(1, 10)
                .Select(x => new Post()
                {
                    PostId = x,
                    userId = x,
                    Description = $"Description {x}",
                    Date = DateTime.Now,
                    ImageUrl = $"https://misapis.com/{x}"
                });
            return posts;
        }
    }
}
