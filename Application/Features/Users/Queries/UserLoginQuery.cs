using Application.Features.Users.DTOs;
using Application.Features.Users.Rules;
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

namespace Application.Features.Users.Queries
{
    public class UserLoginQuery : IRequest<AccessTokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, AccessTokenDto>
        {
            private readonly IMapper mapper;
            private readonly IUserRepository userRepository;
            private readonly ITokenHelper tokenHelper;
            private readonly UserBusinessRules userBusinessRules;

            public UserLoginQueryHandler(IMapper mapper, IUserRepository userRepository, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
            {
                this.mapper = mapper;
                this.userRepository = userRepository;
                this.tokenHelper = tokenHelper;
                this.userBusinessRules = userBusinessRules;
            }

            public async Task<AccessTokenDto> Handle(UserLoginQuery request, CancellationToken cancellationToken)
            {
                User? user = await userRepository.GetAsync(u => u.Email == request.Email,
                    include: u => u.Include(c => c.UserOperationClaims).ThenInclude(c => c.OperationClaim));

                userBusinessRules.UserShouldExistWhenRequested(user);
                userBusinessRules.UserPasswordShouldBeMatch(request.Password, user.PasswordHash, user.PasswordSalt);

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
