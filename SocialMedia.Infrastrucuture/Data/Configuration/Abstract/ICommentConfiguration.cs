using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastrucuture.Data.Configuration.Abstract
{
    public interface ICommentConfiguration: IEntityTypeConfiguration<Comment>
    {
    }
}
