using Application.Features.UserOperationClaims.DTOs;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaims
{
    public class CreateUserOperationClaimCommand : IRequest<CreatedUserOperationClaimDto>, ISecuredRequest
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public string[] Roles { get; } = new string[] { "superuser" };

        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimDto>
        {
            private readonly IMapper mapper;
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly UserOperationClaimBusinessRules userOperationClaimBusinessRules;

            public CreateUserOperationClaimCommandHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                this.mapper = mapper;
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                // await businessrules...

                UserOperationClaim userOperationClaim = mapper.Map<UserOperationClaim>(request);
                UserOperationClaim createdUserOperationClaim = await userOperationClaimRepository.AddAsync(userOperationClaim);
                CreatedUserOperationClaimDto createdUserOperationClaimDto = mapper.Map<CreatedUserOperationClaimDto>(createdUserOperationClaim);
                return createdUserOperationClaimDto;
            }
        }
    }
}
