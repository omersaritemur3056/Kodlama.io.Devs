using Application.Features.Users.DTOs;
using Application.Features.Users.Rules;
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

namespace Application.Features.Users.Commands.CreateUsers
{
    public class RegisterUserCommand : IRequest<CreatedUserDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, CreatedUserDto>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly ITokenHelper tokenHelper;
        private readonly UserBusinessRules userBusinessRules;

        public RegisterUserCommandHandler(IMapper mapper, IUserRepository userRepository, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.tokenHelper = tokenHelper;
            this.userBusinessRules = userBusinessRules;
        }

        public async Task<CreatedUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await userBusinessRules.UserEmailCannotBeDuplicated(request.Email);

            HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user =
                    new()
                    {
                        AuthenticatorType = 0,
                        Email = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Status = true
                    };

            User newUser = await userRepository.AddAsync(user);
            CreatedUserDto createdUserDto = mapper.Map<CreatedUserDto>(user);
            return createdUserDto;
        }
    }
}
