using Application.Features.SocialMedias.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMedias.Queries.GetListSocialMedia
{
    public class GetListSocialMediaQuery : IRequest<GetListSocialMediaModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public string[] Roles { get; } = new string[] { "superuser", "admin" };

        public class GetListSocialMediaQueryHandler : IRequestHandler<GetListSocialMediaQuery, GetListSocialMediaModel>
        {
            private readonly IMapper mapper;
            private readonly ISocialMediaRepository socialMediaRepository;

            public GetListSocialMediaQueryHandler(IMapper mapper, ISocialMediaRepository socialMediaRepository)
            {
                this.mapper = mapper;
                this.socialMediaRepository = socialMediaRepository;
            }

            public async Task<GetListSocialMediaModel> Handle(GetListSocialMediaQuery request, CancellationToken cancellationToken)
            {
                IPaginate<SocialMedia> socialMedias = await socialMediaRepository.GetListAsync(
                    include: m => m.Include(u => u.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);

                var mappedSocialMedia = mapper.Map<GetListSocialMediaModel>(socialMedias);
                return mappedSocialMedia;
            }
        }
    }
}
