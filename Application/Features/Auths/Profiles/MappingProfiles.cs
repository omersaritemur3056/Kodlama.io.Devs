using Application.Features.Auths.Commands.CreateUsers;
using Application.Features.Auths.Commands.LoginUsers;
using Application.Features.Auths.DTOs;
using Application.Features.Auths.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, RegisterUserCommand>().ReverseMap();
            CreateMap<User, UserLogin>().ReverseMap();

            CreateMap<User, GetListUserDto>().ReverseMap();
            CreateMap<IPaginate<User>, GetListUserModel>().ReverseMap();

            CreateMap<ITokenHelper, JwtHelper>().ReverseMap();

        }
    }
}
