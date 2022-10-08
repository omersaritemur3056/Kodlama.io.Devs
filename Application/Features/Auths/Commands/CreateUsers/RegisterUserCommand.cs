using Application.Features.Auths.DTOs;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.CreateUsers
{
    public class RegisterUserCommand : IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }

        
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisteredDto>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService)
        {
            _authBusinessRules = authBusinessRules;
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<RegisteredDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

            User newUser = new()
            {
                Email = request.UserForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = request.UserForRegisterDto.FirstName,
                LastName = request.UserForRegisterDto.LastName,
                Status = true
            };

            User createdUser = await _userRepository.AddAsync(newUser);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);
            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredDto registeredDto = new()
            {
                RefreshToken = addedRefreshToken,
                AccessToken = createdAccessToken,
            };
            return registeredDto;
        }
    }
}
