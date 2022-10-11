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

namespace Application.Features.ProgrammingTechnologies.Queries.GetByIdProgrammingTechnology
{
    public class GetByIdProgrammingTechnologyQuery : IRequest<ProgrammingTechnologyGetByIdDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles { get; } = new string[] {"Admin" };

        public class GetByIdProgrammingTechnologyQueryHandler : IRequestHandler<GetByIdProgrammingTechnologyQuery, ProgrammingTechnologyGetByIdDto>
        {
            private readonly IMapper mapper;
            private readonly IProgrammingTechnologyRepository programmingTechnologyRepository;
            //private readonly ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules;

            public GetByIdProgrammingTechnologyQueryHandler(IMapper mapper, IProgrammingTechnologyRepository programmingTechnologyRepository, ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules)
            {
                this.mapper = mapper;
                this.programmingTechnologyRepository = programmingTechnologyRepository;
                //this.programmingTechnologyBusinessRules = programmingTechnologyBusinessRules;
            }

            public async Task<ProgrammingTechnologyGetByIdDto> Handle(GetByIdProgrammingTechnologyQuery request, CancellationToken cancellationToken)
            {
                ProgrammingTechnology getProgrammingTechnology = await
                    programmingTechnologyRepository.GetAsync(getProgrammingTechnology =>
                    getProgrammingTechnology.Id == request.Id);
                //Businessrules...
                ProgrammingTechnologyGetByIdDto programmingTechnologyGetByIdDto = 
                    mapper.Map<ProgrammingTechnologyGetByIdDto>(getProgrammingTechnology);
                return programmingTechnologyGetByIdDto;
            }
        }
    }
}
