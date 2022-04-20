using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastrucuture.Data;
using System.Threading.Tasks;

namespace SocialMedia.Infrastrucuture.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(SocialMediaContext dbContext) : base(dbContext) { }

        public async Task<Security> GetLoginByCredentials(UserLogin userLogin)
        {
            return await _dbContext.Securities.FirstOrDefaultAsync(x => x.User == userLogin.UserName && x.Password == userLogin.Password);
        }
    }
}
