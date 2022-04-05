using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastrucuture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastrucuture.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(SocialMediaContext dbContext) : base(dbContext){ }

        public async Task<IEnumerable<Post>> GetPostsByUserId(int userId)
        {
            return await _dbContext.Posts.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
