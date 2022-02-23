using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialMedia.Core.Entities;
using SocialMedia.Infrastrucuture.Data.Configuration;
using SocialMedia.Infrastrucuture.Data.Configuration.Abstract;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SocialMedia.Infrastrucuture.Data
{
    public partial class SocialMediaContext : DbContext
    {
        private readonly IUserConfiguration _userConfiguration;
        private readonly ICommentConfiguration _commentConfiguration;
        private readonly IPostConfiguration _postConfiguration;
        public IConfiguration Configuration { get; }
        public SocialMediaContext(
            IUserConfiguration userConfiguration, 
            IConfiguration configuration,
            ICommentConfiguration commentConfiguration,
            IPostConfiguration postConfiguration)
        {
            Configuration = configuration;
            _userConfiguration = userConfiguration;
            _commentConfiguration = commentConfiguration;
            _postConfiguration = postConfiguration;
        }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SocialMediaDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // comment entity
            modelBuilder.ApplyConfiguration(_commentConfiguration);
            // post entity
            modelBuilder.ApplyConfiguration(_postConfiguration);
            // user entity
            modelBuilder.ApplyConfiguration(_userConfiguration);
        }
    }
}
