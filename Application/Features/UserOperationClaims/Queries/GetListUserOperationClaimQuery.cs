using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries
{
    public class GetListUserOperationClaimQuery : IRequest<GetListUserOperationClaimModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQuery, GetListUserOperationClaimModel>
        {
            private readonly IMapper mapper;
            private readonly IUserOperationClaimRepository userOperationClaimRepository;

            public GetListUserOperationClaimQueryHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository)
            {
                this.mapper = mapper;
                this.userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<GetListUserOperationClaimModel> Handle(GetListUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> userOperationClaims = await userOperationClaimRepository.GetListAsync(
                    include: u => u.Include(u => u.User).Include(u => u.OperationClaim),
                   index: request.PageRequest.Page,
                   size: request.PageRequest.PageSize);

                GetListUserOperationClaimModel mappedGetListUserOperationClaimModel = mapper.Map<GetListUserOperationClaimModel>(userOperationClaims);
                return mappedGetListUserOperationClaimModel;
            }
        }
    }
}
