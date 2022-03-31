using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastrucuture.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infrastrucuture.Repositories
{
    public class PostRepository: IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<Post> GetPostById(int Id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == Id);
            return post;
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        public async Task InsertPost(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            _context.Attach(post);
            _context.Entry(post).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeletePost(int Id)
        {
            var currentPost = await GetPostById(Id);
            _context.Entry(currentPost).State = EntityState.Deleted;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
