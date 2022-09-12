using Application.Features.ProgrammingTechnologies.Commands.CreateProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.Commands.DeleteProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.DTOs;
using Application.Features.ProgrammingTechnologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingTechnologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProgrammingTechnology, CreatedProgrammingTechnologyDto>().ReverseMap();
            CreateMap<ProgrammingTechnology, CreateProgrammingTechnologyCommand>().ReverseMap();

            CreateMap<ProgrammingTechnology, DeletedProgrammingTechnologyByIdDto>().ReverseMap();
            CreateMap<ProgrammingTechnology, DeleteProgrammingTechnologyCommand>().ReverseMap();

            CreateMap<ProgrammingTechnology, UpdatedProgrammingTechnologyDto>().ReverseMap();
            CreateMap<ProgrammingTechnology, UpdateProgrammingTechnologyCommand>().ReverseMap();

            CreateMap<IPaginate<ProgrammingTechnology>, ProgrammingTechnologyListModel>().ReverseMap();

            CreateMap<ProgrammingTechnology, ProgrammingTechnologyListDto>()
                .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();

            CreateMap<ProgrammingTechnology, ProgrammingTechnologyGetByIdDto>()
                .ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();


        }
    }
}
