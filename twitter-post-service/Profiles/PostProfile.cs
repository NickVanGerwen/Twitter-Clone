﻿using AutoMapper;

namespace twitter_post_service.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<DTOs.PostCreateDTO, Models.Post>();
        }
    }
}
