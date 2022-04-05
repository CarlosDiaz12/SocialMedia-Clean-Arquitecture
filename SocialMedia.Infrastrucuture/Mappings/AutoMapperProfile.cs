using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastrucuture.Mappings
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(a => a.PostId, b => b.MapFrom(p => p.Id));
            CreateMap<PostDto, Post>()
                .ForMember(a => a.Id, b => b.MapFrom(p => p.PostId));
        }
    }
}
