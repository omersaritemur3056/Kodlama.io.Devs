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

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task UserEmailCannotBeDuplicated(string email)
        {
            IPaginate<User> result = await userRepository.GetListAsync(u => u.Email == email);
            if (result.Items.Any())
                throw new BusinessException("This email used for registiration.");
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
    }
}
