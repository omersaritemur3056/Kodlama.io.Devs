using Application.Features.Auths.DTOs;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.CreateUsers
{
    public class UserLogin : IRequest<AccessTokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class UserLoginHandler : IRequestHandler<UserLogin, AccessTokenDto>
        {
            private readonly IMapper mapper;
            private readonly IUserRepository userRepository;
            private readonly ITokenHelper tokenHelper;
            private readonly AuthBusinessRules authBusinessRules;

            public UserLoginHandler(IMapper mapper, IUserRepository userRepository, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules)
            {
                this.mapper = mapper;
                this.userRepository = userRepository;
                this.tokenHelper = tokenHelper;
                this.authBusinessRules = authBusinessRules;
            }

            public async Task<AccessTokenDto> Handle(UserLogin request, CancellationToken cancellationToken)
            {
                User? user = await userRepository.GetAsync(u => u.Email == request.Email,
                    include: u => u.Include(c => c.UserOperationClaims).ThenInclude(c => c.OperationClaim));

                authBusinessRules.UserShouldExistWhenRequested(user);
                authBusinessRules.UserPasswordShouldBeMatch(request.Password, user.PasswordHash, user.PasswordSalt);

                List<OperationClaim> operationClaims = new();

                foreach (var userOperationClaim in user.UserOperationClaims)
                {
                    operationClaims.Add(userOperationClaim.OperationClaim);
                }

                AccessToken accessToken = tokenHelper.CreateToken(user, operationClaims);
                AccessTokenDto accessTokenDto = mapper.Map<AccessTokenDto>(accessToken);
                return accessTokenDto;
            }
        }
    }
}
