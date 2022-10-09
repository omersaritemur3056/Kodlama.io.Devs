using Application.Features.OperationClaims.DTOs;
using Application.Features.OperationClaims.Rules;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.UpdatedOperationClaims
{
    public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimDto>
        {
            private readonly IMapper mapper;
            private readonly IOperationClaimRepository operationClaimRepository;
            private readonly OperationClaimBusinessRules operationClaimBusinessRules;

            public UpdateOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                this.mapper = mapper;
                this.operationClaimRepository = operationClaimRepository;
                this.operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim operationClaim = mapper.Map<OperationClaim>(request);

                operationClaimBusinessRules.OperationClaimMustBeExistWhenRequested(operationClaim);

                OperationClaim updatedOperationClaim = await operationClaimRepository.UpdateAsync(operationClaim);
                UpdatedOperationClaimDto mappedUpdatedOperationClaimDto = mapper.Map<UpdatedOperationClaimDto>(updatedOperationClaim);
                return mappedUpdatedOperationClaimDto;
            }
        }
    }
}
