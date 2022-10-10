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

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguages
{
    public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDto>, ISecuredRequest
    {
        public string Name { get; set; }
        public string[] Roles { get; } = new string[] { "superuser" };

        public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<
            DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

            public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programmingLanguageBusinessRules.ProgrammingLanguageMustBeExistForDelete(request.Name);

                ProgrammingLanguage programmingLanguage = await _programmingLanguageRepository
                    .GetAsync(pl => pl.Name == request.Name);
                ProgrammingLanguage deletedProgrammingLanguage = await 
                    _programmingLanguageRepository.DeleteAsync(programmingLanguage);
                DeletedProgrammingLanguageDto deletedProgrammingLanguageDto = 
                    _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);
                return deletedProgrammingLanguageDto;
            }
        }
    }
}
