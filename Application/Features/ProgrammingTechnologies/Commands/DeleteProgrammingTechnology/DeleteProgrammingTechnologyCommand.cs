using Application.Features.ProgrammingTechnologies.DTOs;
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

namespace Application.Features.ProgrammingTechnologies.Commands.DeleteProgrammingTechnology
{
    public class DeleteProgrammingTechnologyCommand : IRequest<DeletedProgrammingTechnologyByIdDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles { get; } = new string[] { "superuser" };

        public class DeleteProgrammingTechnologyCommandHandler : IRequestHandler<DeleteProgrammingTechnologyCommand, DeletedProgrammingTechnologyByIdDto>
        {
            private readonly IMapper mapper;
            private readonly IProgrammingTechnologyRepository programmingTechnologyRepository;

            public DeleteProgrammingTechnologyCommandHandler(IMapper mapper, IProgrammingTechnologyRepository programmingTechnologyRepository)
            {
                this.mapper = mapper;
                this.programmingTechnologyRepository = programmingTechnologyRepository;
            }

            public async Task<DeletedProgrammingTechnologyByIdDto> Handle(DeleteProgrammingTechnologyCommand request, CancellationToken cancellationToken)
            {
                ProgrammingTechnology mapperProgrammingTechnology = mapper.Map<ProgrammingTechnology>(request);
                ProgrammingTechnology deletedProgrammingTechnology = await 
                    programmingTechnologyRepository.DeleteAsync(mapperProgrammingTechnology);
                DeletedProgrammingTechnologyByIdDto deletedProgrammingTechnologyByIdDto = 
                    mapper.Map<DeletedProgrammingTechnologyByIdDto>(deletedProgrammingTechnology);
                return deletedProgrammingTechnologyByIdDto;
            }
        }
    }
}
