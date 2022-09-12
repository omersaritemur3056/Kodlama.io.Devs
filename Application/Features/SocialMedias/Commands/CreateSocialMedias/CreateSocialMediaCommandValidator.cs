using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMedias.Commands.CreateSocialMedias
{
    public class CreateSocialMediaCommandValidator : AbstractValidator<CreateSocialMediaCommand>
    {
        public CreateSocialMediaCommandValidator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.Url).NotEmpty();
            //daha yazılabilir...
        }
    }
}
