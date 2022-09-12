using Application.Features.SocialMedias.Commands.CreateSocialMedias;
using Application.Features.SocialMedias.Commands.DeleteSocialMedias;
using Application.Features.SocialMedias.Commands.UpdateSocialMedias;
using Application.Features.SocialMedias.DTOs;
using Application.Features.SocialMedias.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMedias.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SocialMedia, GetListSocialMediaDto>()
                .ForMember(s => s.FirstName, opt => opt.MapFrom(c => c.User.FirstName))
                .ForMember(s => s.LastName, opt => opt.MapFrom(c => c.User.LastName))
                .ReverseMap();
            CreateMap<IPaginate<SocialMedia>, GetListSocialMediaModel>().ReverseMap();

            CreateMap<SocialMedia, CreatedSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, CreateSocialMediaCommand>().ReverseMap();

            CreateMap<SocialMedia, DeletedSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, DeleteSocialMediaCommand>().ReverseMap();

            CreateMap<SocialMedia, UpdatedSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, UpdateSocialMediaCommand>().ReverseMap();


        }
    }
}
