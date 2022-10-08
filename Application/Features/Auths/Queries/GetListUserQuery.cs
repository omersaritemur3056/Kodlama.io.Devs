using Application.Features.Auths.Models;
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

namespace Application.Features.Auths.Queries
{
    public class GetListUserQuery : IRequest<GetListUserModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListUserModel>
        {
            private readonly IMapper mapper;
            private readonly IUserRepository userRepository;

            public GetListUserQueryHandler(IMapper mapper, IUserRepository userRepository)
            {
                this.mapper = mapper;
                this.userRepository = userRepository;
            }

            public async Task<GetListUserModel> Handle(GetListUserQuery request, CancellationToken cancellationToken)
            {
                IPaginate<User> users = await userRepository.GetListAsync(
                    //include: u => u.Include(u => u.AuthenticatorType),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);

                GetListUserModel mappedGetListUserModel = mapper.Map<GetListUserModel>(users);
                return mappedGetListUserModel;
            }
        }
    }
}
