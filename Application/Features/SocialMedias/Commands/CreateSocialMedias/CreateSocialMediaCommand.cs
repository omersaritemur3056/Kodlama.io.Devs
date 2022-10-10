using Application.Features.SocialMedias.DTOs;
using Application.Features.SocialMedias.Rules;
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

namespace Application.Features.SocialMedias.Commands.CreateSocialMedias
{
    public class CreateSocialMediaCommand : IRequest<CreatedSocialMediaDto>, ISecuredRequest
    {
        public string Url { get; set; }
        public int UserId { get; set; }
        public string[] Roles { get; } = new string[] { "superuser" };

        public class CreateSocialMediaCommandHandler : IRequestHandler<CreateSocialMediaCommand, CreatedSocialMediaDto>
        {
            private readonly IMapper mapper;
            private readonly ISocialMediaRepository socialMediaRepository;
            private readonly SocialMediaBusinessRules socialMediaBusinessRules;

            public CreateSocialMediaCommandHandler(IMapper mapper, ISocialMediaRepository socialMediaRepository, SocialMediaBusinessRules socialMediaBusinessRules)
            {
                this.mapper = mapper;
                this.socialMediaRepository = socialMediaRepository;
                this.socialMediaBusinessRules = socialMediaBusinessRules;
            }

            public async Task<CreatedSocialMediaDto> Handle(CreateSocialMediaCommand request, CancellationToken cancellationToken)
            {
                await socialMediaBusinessRules.SocialMediaLinkCannotBeDuplicated(request.Url);

                var socialMedia = mapper.Map<SocialMedia>(request);
                SocialMedia createdLink = await socialMediaRepository.AddAsync(socialMedia);
                var mappedLink = mapper.Map<CreatedSocialMediaDto>(createdLink);
                return mappedLink;
            }
        }
    }
}
