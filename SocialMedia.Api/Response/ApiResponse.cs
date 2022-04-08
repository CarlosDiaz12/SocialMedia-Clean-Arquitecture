using SocialMedia.Core.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Response
{
    public class ApiResponse<TResult>
    {
        public TResult Data { get; set; }
        public Metadata Meta { get; set; }
        public ApiResponse(TResult data, Metadata metadata = null)
        {
            Data = data;
            Meta = metadata;
        }
    }
}
