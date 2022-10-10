using Application.Features.ProgrammingLanguages.DTOs;
using Application.Features.ProgrammingLanguages.Rules;
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

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguages
{
    public class CreateProgrammingLanguageCommand : IRequest<CreatedProgrammingLanguageDto>, ISecuredRequest
    {
        public string Name { get; set; }
        public string[] Roles { get; } = new string[] { "superuser" };

        public class CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand,CreatedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

            public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programmingLanguageBusinessRules
                    .ProgrammingLanguageNameCanNotBeDuplicatedInserted(request.Name);

                ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage createdProgrammingLanguage = await 
                    _programmingLanguageRepository.AddAsync(mappedProgrammingLanguage);

                CreatedProgrammingLanguageDto createdProgrammingLanguageDto = 
                    _mapper.Map<CreatedProgrammingLanguageDto>(createdProgrammingLanguage);

                return createdProgrammingLanguageDto;
            }
        }
    }
}
