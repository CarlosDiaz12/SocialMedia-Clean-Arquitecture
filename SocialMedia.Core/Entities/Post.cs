using System;
namespace SocialMedia.Core.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public int userId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
