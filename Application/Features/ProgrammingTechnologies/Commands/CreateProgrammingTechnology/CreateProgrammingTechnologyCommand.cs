using Application.Features.ProgrammingTechnologies.DTOs;
using Application.Features.ProgrammingTechnologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingTechnologies.Commands.CreateProgrammingTechnology
{
    public class CreateProgrammingTechnologyCommand : IRequest<CreatedProgrammingTechnologyDto>, ISecuredRequest
    {
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string[] Roles { get; } = new string[] { "Admin" };

        public class CreateProgrammingTechnologyCommandHandler : IRequestHandler<CreateProgrammingTechnologyCommand, CreatedProgrammingTechnologyDto>
        {
            private readonly IMapper mapper;
            private readonly IProgrammingTechnologyRepository programmingTechnologyRepository;
            //private readonly ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules;

            public CreateProgrammingTechnologyCommandHandler(IMapper mapper, IProgrammingTechnologyRepository programmingTechnologyRepository, ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules)
            {
                this.mapper = mapper;
                this.programmingTechnologyRepository = programmingTechnologyRepository;
                //this.programmingTechnologyBusinessRules = programmingTechnologyBusinessRules;
            }

            public async Task<CreatedProgrammingTechnologyDto> Handle(CreateProgrammingTechnologyCommand request, CancellationToken cancellationToken)
            {
                //businessrules...

                ProgrammingTechnology mapperProgrammingTechnology = mapper.Map<ProgrammingTechnology>(request);

                ProgrammingTechnology createdProgrammingTechnology = await 
                    programmingTechnologyRepository.AddAsync(mapperProgrammingTechnology);
                CreatedProgrammingTechnologyDto createdProgrammingTechnologyDto =
                    mapper.Map<CreatedProgrammingTechnologyDto>(createdProgrammingTechnology);
                return createdProgrammingTechnologyDto;
            }
        }
    }
}
