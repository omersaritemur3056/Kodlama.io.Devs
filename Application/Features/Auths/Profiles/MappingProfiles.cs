using Application.Features.Auths.Commands.CreateUsers;
using Application.Features.Auths.DTOs;
using AutoMapper;
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
            CreateMap<AccessToken, AccessTokenDto>().ReverseMap();
            CreateMap<User, UserLogin>().ReverseMap();

            CreateMap<ITokenHelper, JwtHelper>().ReverseMap();
        }
    }
}
