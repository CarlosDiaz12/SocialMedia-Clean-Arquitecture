using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastrucuture.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}
