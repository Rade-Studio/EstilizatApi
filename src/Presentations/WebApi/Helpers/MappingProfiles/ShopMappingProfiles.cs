using System.Collections.Generic;
using AutoMapper;
using Models.DbEntities;
using Models.DTOs.Shop;
using Models.ResponseModels.Shop;

namespace WebApi.Helpers.MappingProfiles
{
    public class ShopMappingProfile : Profile
    {
        public ShopMappingProfile()
        {
            // Shop
            CreateMap<Shop, ShopDto>();
            
            // Reviews
            // CreateMap<ShopReview, ShopReviewDto>()
            //     .ForMember(x => x.Content, opt => opt.MapFrom(y => y.Review))
            //     .ForMember(x => x.Rating, opt => opt.MapFrom(y => y.Rating));
            // CreateMap<IReadOnlyList<ShopReview>, IReadOnlyList<ShopReviewDto>>()
            //     .ReverseMap();
        }
    }
}