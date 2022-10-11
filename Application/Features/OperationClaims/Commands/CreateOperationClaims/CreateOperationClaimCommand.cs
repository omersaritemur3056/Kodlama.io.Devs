using Application.Features.OperationClaims.DTOs;
using Application.Features.OperationClaims.Rules;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.CreateOperationClaims
{
    public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimDto>
    {
        public string Name { get; set; }
        //public string[] Roles { get; } = new string[] { "superuser" };

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
        {
            private readonly IMapper mapper;
            private readonly IOperationClaimRepository operationClaimRepository;
            private readonly OperationClaimBusinessRules operationClaimBusinessRules;

            public CreateOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                this.mapper = mapper;
                this.operationClaimRepository = operationClaimRepository;
                this.operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await operationClaimBusinessRules.OperationClaimCanNotBeDuplicated(request.Name);

                OperationClaim operationClaim = mapper.Map<OperationClaim>(request);
                OperationClaim createdOperationClaim = await operationClaimRepository.AddAsync(operationClaim);
                CreatedOperationClaimDto createdOperationClaimDto = mapper.Map<CreatedOperationClaimDto>(createdOperationClaim);
                return createdOperationClaimDto;
            }
        }
    }
}
