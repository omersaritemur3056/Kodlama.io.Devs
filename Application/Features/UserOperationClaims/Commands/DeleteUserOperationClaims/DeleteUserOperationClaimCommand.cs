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

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaims
{
    public class DeleteUserOperationClaimCommand : IRequest<DeletedUserOperationClaimDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles { get; } = new string[] { "superuser" };

        public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimDto>
        {
            private readonly IMapper mapper;
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly UserOperationClaimBusinessRules userOperationClaimBusinessRules;

            public DeleteUserOperationClaimCommandHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                this.mapper = mapper;
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<DeletedUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                UserOperationClaim userOperationClaim = await userOperationClaimRepository.GetAsync(u => u.User.Id == request.Id);
                UserOperationClaim mappedUserOperationClaim = mapper.Map<UserOperationClaim>(userOperationClaim);
                UserOperationClaim deletedUserOperationClaim = await userOperationClaimRepository.DeleteAsync(mappedUserOperationClaim);
                DeletedUserOperationClaimDto deletedUserOperationClaimDto = mapper.Map<DeletedUserOperationClaimDto>(deletedUserOperationClaim);

                return deletedUserOperationClaimDto;
            }
        }
    }
}
