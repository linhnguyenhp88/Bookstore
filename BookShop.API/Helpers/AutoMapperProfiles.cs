using AutoMapper;
using BookShop.Domain.Dtos;
using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<Book, BookForDetailedDto>()
                .ForMember(dest => dest.AuthorName, opt =>
                {
                    opt.MapFrom(src => src.Author.Name);
                }).ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault().Url);
                });

            CreateMap<Book, BookForListDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault().Url);
                });               
        }
    }
}

