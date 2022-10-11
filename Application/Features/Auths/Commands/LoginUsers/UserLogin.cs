using Application.Features.Auths.DTOs;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
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

namespace Application.Features.Auths.Commands.LoginUsers
{
    public class UserLogin : IRequest<LoginedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public class UserLoginHandler : IRequestHandler<UserLogin, LoginedDto>
        {
            private readonly IUserRepository userRepository;
            private readonly IAuthService authService;
            private readonly AuthBusinessRules authBusinessRules;

            public UserLoginHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules)
            {
                this.userRepository = userRepository;
                this.authService = authService;
                this.authBusinessRules = authBusinessRules;
            }

            public async Task<LoginedDto> Handle(UserLogin request, CancellationToken cancellationToken)
            {
                User? userToLogin = await userRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email); //burayı ayrıca incele

                await authBusinessRules.UserShouldBeExistWhenLogin(request.UserForLoginDto.Email);
                authBusinessRules.UserPasswordShouldBeMatch(request.UserForLoginDto.Password, userToLogin.PasswordHash, userToLogin.PasswordSalt); //ayrıca incele

                AccessToken accessToken = await authService.CreateAccessToken(userToLogin);

                return new LoginedDto { AccessToken = accessToken };
            }
        }
    }
}
