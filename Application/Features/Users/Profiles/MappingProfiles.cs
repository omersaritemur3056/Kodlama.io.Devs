using Application.Features.Users.Commands.CreateUsers;
using Application.Features.Users.DTOs;
using Application.Features.Users.Queries;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, CreatedUserDto>().ReverseMap();
            CreateMap<User, RegisterUserCommand>().ReverseMap();
            CreateMap<AccessToken, AccessTokenDto>().ReverseMap();
            CreateMap<User, UserLoginQuery>().ReverseMap();

            CreateMap<ITokenHelper, JwtHelper>().ReverseMap();
        }
    }
}
