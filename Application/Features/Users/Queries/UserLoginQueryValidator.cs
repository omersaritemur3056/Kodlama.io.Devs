using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class UserLoginQueryValidator : AbstractValidator<UserLoginQuery>
    {
        public UserLoginQueryValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.Password).MinimumLength(8);
            RuleFor(c => c.Password).Must(ContainLDS);
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
