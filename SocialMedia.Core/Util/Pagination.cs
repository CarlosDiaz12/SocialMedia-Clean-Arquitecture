using SocialMedia.Core.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialMedia.Core.Util
{
    public class Pagination<T>
    {
        public static PagedResult<T> GetPagedResultForQuery(IEnumerable<T> query, int page, int pageSize, bool paginateResults = true)
        {
            var pageCount = (double)query.Count() / pageSize;
            var skip = (page - 1) * pageSize;
            var result = query;
            if (paginateResults)
                result = result.Skip(skip).Take(pageSize);

            return new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count(),
                PageCount = (int)Math.Ceiling(pageCount),
                Results = result
            };
        }
    }
}
