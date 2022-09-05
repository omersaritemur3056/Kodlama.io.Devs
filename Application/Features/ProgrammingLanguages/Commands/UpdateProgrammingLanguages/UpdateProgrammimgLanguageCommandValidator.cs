using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguages
{
    public class UpdateProgrammimgLanguageCommandValidator : AbstractValidator<UpdateProgrammingLanguageCommand>
    {
        public UpdateProgrammimgLanguageCommandValidator()
        {
            RuleFor(pl => pl.Id).NotEmpty();
            RuleFor(pl => pl.Name).NotEmpty();
            RuleFor(pl => pl.Id).GreaterThan(0);
        }
    }
}
