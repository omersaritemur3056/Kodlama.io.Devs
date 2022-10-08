using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.CreateUsers
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.UserForRegisterDto.Email).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.Email).EmailAddress();
            RuleFor(c => c.UserForRegisterDto.Password).NotEmpty();
            RuleFor(c => c.UserForRegisterDto.Password).MinimumLength(8);
            RuleFor(c => c.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(c => c.UserForRegisterDto.FirstName).MinimumLength(2);
            RuleFor(c => c.UserForRegisterDto.LastName).NotEmpty();
            RuleFor(c => c.UserForRegisterDto.LastName).MinimumLength(2);
        }

        
    }
}
