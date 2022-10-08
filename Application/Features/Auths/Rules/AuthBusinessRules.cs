using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public void UserPasswordShouldBeMatch(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
            if (!result)
            {
                throw new BusinessException("password doesn't match.");
            }
        }

        public void UserShouldExistWhenRequested(User user)
        {
            if (user == null)
            {
                throw new BusinessException("User does not exist");
            }
        }

        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            User? user = await userRepository.GetAsync(u => u.Email == email);
            if (user != null)
            {
                throw new BusinessException("Mail already exist");
            }
        }


    }
}
