using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastrucuture.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastrucuture.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        public UnitOfWork(
            SocialMediaContext context,
            IPostRepository postRepository,
            IRepository<User> userRepository,
            IRepository<Comment> commentRepository,
            ISecurityRepository securityRepository)
        {
            _context = context;
            PostRepository = postRepository;
            UserRepository = userRepository;
            CommentRepository = commentRepository;
            SecurityRepository = securityRepository;
        }
        public IPostRepository PostRepository { get; }

        public IRepository<User> UserRepository { get; }

        public IRepository<Comment> CommentRepository { get; }

        public ISecurityRepository SecurityRepository { get; }

        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
