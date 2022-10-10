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

namespace Application.Features.SocialMedias.Commands.UpdateSocialMedias
{
    public class UpdateSocialMediaCommand : IRequest<UpdatedSocialMediaDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        public string[] Roles { get; } = new string[] { "superuser" };

        public class UpdateSocialMediaCommandHandler : IRequestHandler<UpdateSocialMediaCommand, UpdatedSocialMediaDto>
        {
            private readonly IMapper mapper;
            private readonly ISocialMediaRepository socialMediaRepository;
            private readonly SocialMediaBusinessRules socialMediaBusinessRules;

            public UpdateSocialMediaCommandHandler(IMapper mapper, ISocialMediaRepository socialMediaRepository, SocialMediaBusinessRules socialMediaBusinessRules)
            {
                this.mapper = mapper;
                this.socialMediaRepository = socialMediaRepository;
                this.socialMediaBusinessRules = socialMediaBusinessRules;
            }

            public async Task<UpdatedSocialMediaDto> Handle(UpdateSocialMediaCommand request, CancellationToken cancellationToken)
            {
                SocialMedia socialMedia = mapper.Map<SocialMedia>(request);

                socialMediaBusinessRules.SocialMediaLinkMustBeExistWhenRequested(socialMedia);

                SocialMedia updatedLink = await socialMediaRepository.UpdateAsync(socialMedia);
                var mappedLink = mapper.Map<UpdatedSocialMediaDto>(updatedLink);
                return mappedLink;
            }
        }
    }
}
