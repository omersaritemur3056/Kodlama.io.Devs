using Application.Features.SocialMedias.DTOs;
using Application.Features.SocialMedias.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMedias.Commands.DeleteSocialMedias
{
    public class DeleteSocialMediaCommand : IRequest<DeletedSocialMediaDto>
    {
        public int Id { get; set; }

        public class DeleteSocialMediaCommandHandler : IRequestHandler<DeleteSocialMediaCommand, DeletedSocialMediaDto>
        {
            private readonly IMapper mapper;
            private readonly ISocialMediaRepository socialMediaRepository;
            private readonly SocialMediaBusinessRules socialMediaBusinessRules;

            public DeleteSocialMediaCommandHandler(IMapper mapper, ISocialMediaRepository socialMediaRepository, SocialMediaBusinessRules socialMediaBusinessRules)
            {
                this.mapper = mapper;
                this.socialMediaRepository = socialMediaRepository;
                this.socialMediaBusinessRules = socialMediaBusinessRules;
            }

            public async Task<DeletedSocialMediaDto> Handle(DeleteSocialMediaCommand request, CancellationToken cancellationToken)
            {
                SocialMedia socialMedia = await socialMediaRepository.GetAsync(s => s.Id == request.Id);

                await socialMediaBusinessRules.SocialMediaLinkMustBeExistWhenRequested(socialMedia);

                SocialMedia deletedLink = await socialMediaRepository.DeleteAsync(socialMedia);
                var mappedLink = mapper.Map<DeletedSocialMediaDto>(deletedLink);
                return mappedLink;
            }
        }
    }
}
