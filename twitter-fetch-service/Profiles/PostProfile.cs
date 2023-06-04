using AutoMapper;

namespace twitter_fetch_service.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Models.Post, DTOs.PostReadDTO>();
        }
    }
}
