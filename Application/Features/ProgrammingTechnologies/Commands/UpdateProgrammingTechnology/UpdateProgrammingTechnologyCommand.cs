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

namespace Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnology
{
    public class UpdateProgrammingTechnologyCommand : IRequest<UpdatedProgrammingTechnologyDto>, ISecuredRequest
    {
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string[] Roles { get; } = new string[] { "Admin" };

        public class UpdateProgrammingTechnologyCommandHandler : IRequestHandler<UpdateProgrammingTechnologyCommand, UpdatedProgrammingTechnologyDto>
        {
            private readonly IMapper mapper;
            private readonly IProgrammingTechnologyRepository programmingTechnologyRepository;
            //private readonly ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules;

            public UpdateProgrammingTechnologyCommandHandler(IMapper mapper, IProgrammingTechnologyRepository programmingTechnologyRepository, ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules)
            {
                this.mapper = mapper;
                this.programmingTechnologyRepository = programmingTechnologyRepository;
                //this.programmingTechnologyBusinessRules = programmingTechnologyBusinessRules;
            }

            public async Task<UpdatedProgrammingTechnologyDto> Handle(UpdateProgrammingTechnologyCommand request, CancellationToken cancellationToken)
            {
                //businessrules...

                ProgrammingTechnology mappedProgrammingTechnology = mapper.Map<ProgrammingTechnology>(request);
                ProgrammingTechnology updatedProgrammingTechnology = await 
                    programmingTechnologyRepository.UpdateAsync(mappedProgrammingTechnology);
                UpdatedProgrammingTechnologyDto updatedProgrammingTechnologyDto = 
                    mapper.Map<UpdatedProgrammingTechnologyDto>(updatedProgrammingTechnology);
                return updatedProgrammingTechnologyDto;
            }
        }
    }
}
