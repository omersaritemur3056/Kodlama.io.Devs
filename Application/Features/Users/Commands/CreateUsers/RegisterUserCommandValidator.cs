using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.CreateUsers
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.Password).MinimumLength(8);
            RuleFor(c => c.Password).Must(ContainLDS);
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.FirstName).MinimumLength(2);
            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.LastName).MinimumLength(2);
        }

        private bool ContainLDS(string arg)
        {
            return arg is not null
                && arg.Any(a => char.IsUpper(a))
                && arg.Any(a => char.IsLower(a))
                && arg.Any(a => char.IsDigit(a))
                && arg.Any(a => char.IsSymbol(a) || char.IsPunctuation(a));
        }
    }
}
