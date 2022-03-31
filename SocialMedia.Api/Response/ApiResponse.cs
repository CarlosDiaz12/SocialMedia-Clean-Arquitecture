using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Response
{
    public class ApiResponse<TResult>
    {
        public TResult Data { get; set; }
        public ApiResponse(TResult data)
        {
            Data = data;
        }
    }
}
