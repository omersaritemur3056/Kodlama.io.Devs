using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMedias.Rules
{
    public class SocialMediaBusinessRules
    {
        private readonly ISocialMediaRepository socialMediaRepository;

        public SocialMediaBusinessRules(ISocialMediaRepository socialMediaRepository)
        {
            this.socialMediaRepository = socialMediaRepository;
        }

        public async Task SocialMediaLinkCannotBeDuplicated(string Url)
        {
            SocialMedia result = await socialMediaRepository.GetAsync(s => s.Url == Url);
            if (result != null) throw new BusinessException("Social Link cannot be duplicated");
        }
        public async Task SocialMediaLinkMustBeExistWhenRequested(SocialMedia socialMedia)
        {
            if (socialMedia == null) throw new BusinessException("Social Link does not exist");
        }
    }
}
