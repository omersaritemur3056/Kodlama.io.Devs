using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.EmailAuthenticator
{
    public interface IEmailAuthenticatorHelper
    {
        public Task<string> CreateEmailActivationKey();
        public Task<string> CreateEmailActivationCode();
    }
}
