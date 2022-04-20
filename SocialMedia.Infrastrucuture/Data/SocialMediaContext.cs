using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialMedia.Core.Entities;
using System.Reflection;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SocialMedia.Infrastrucuture.Data
{
    public partial class SocialMediaContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public SocialMediaContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Security> Securities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SocialMediaDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // another way
             modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
