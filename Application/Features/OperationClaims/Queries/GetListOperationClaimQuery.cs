using Application.Features.OperationClaims.Models;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries
{
    public class GetListOperationClaimQuery : IRequest<GetListOperationClaimModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, GetListOperationClaimModel>
        {
            private readonly IMapper mapper;
            private readonly IOperationClaimRepository operationClaimRepository;

            public GetListOperationClaimQueryHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository)
            {
                this.mapper = mapper;
                this.operationClaimRepository = operationClaimRepository;
            }

            public async Task<GetListOperationClaimModel> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> opeartionClaims = await operationClaimRepository.GetListAsync(
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);

                GetListOperationClaimModel mappedGetListOperationClaimModel = mapper.Map<GetListOperationClaimModel>(opeartionClaims);
                return mappedGetListOperationClaimModel;        
            }
        }
    }
}
