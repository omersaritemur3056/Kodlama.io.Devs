using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.CreateUsers
{
    public class UserLoginValidator : AbstractValidator<UserLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.Password).MinimumLength(8);
        }

    }
}
