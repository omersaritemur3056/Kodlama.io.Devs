using Application.Features.OperationClaims.Models;
using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaims;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaims;
using Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaims;
using Application.Features.UserOperationClaims.DTOs;
using Application.Features.UserOperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserOperationClaim, GetListUserOperationClaimDto>()
                .ForMember(u => u.FirstName, opt => opt.MapFrom(u => u.User.FirstName))
                .ForMember(u => u.LastName, opt => opt.MapFrom(u => u.User.LastName))
                .ForMember(u => u.OperationClaimName, opt => opt.MapFrom(u => u.OperationClaim.Name))
                .ReverseMap();
            CreateMap<IPaginate<UserOperationClaim>, GetListUserOperationClaimModel>().ReverseMap();

            CreateMap<UserOperationClaim, CreatedUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();

            CreateMap<UserOperationClaim, DeletedUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, DeleteUserOperationClaimCommand>().ReverseMap();

            CreateMap<UserOperationClaim, UpdatedUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
        }
    }
}
