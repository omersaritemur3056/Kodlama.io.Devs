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

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaims
{
    public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimDto>
    {
        public string Name { get; set; }

        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
        {
            private readonly IMapper mapper;
            private readonly IOperationClaimRepository operationClaimRepository;
            private readonly OperationClaimBusinessRules operationClaimBusinessRules;

            public DeleteOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                this.mapper = mapper;
                this.operationClaimRepository = operationClaimRepository;
                this.operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim operationClaim = await operationClaimRepository.GetAsync(o => o.Name == request.Name);

                operationClaimBusinessRules.OperationClaimMustBeExistWhenRequested(operationClaim);

                OperationClaim deletedOperationClaim = await operationClaimRepository.DeleteAsync(operationClaim);
                DeletedOperationClaimDto deletedOperationClaimDto = mapper.Map<DeletedOperationClaimDto>(deletedOperationClaim);
                return deletedOperationClaimDto;
            }
        }
    }
}
