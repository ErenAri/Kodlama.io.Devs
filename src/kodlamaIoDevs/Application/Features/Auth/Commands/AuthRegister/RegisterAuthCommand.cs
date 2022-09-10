﻿using Application.Features.Auth.Commands.AuthLogin;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
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

namespace Application.Features.Auth.Commands.AuthRegister
{
    public class RegisterAuthCommand:IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class RegisterAuthCommandHandler : IRequestHandler<RegisterAuthCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBusinessRules _authBusinessRules;



            public async Task<AccessToken> Handle(RegisterAuthCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.AuthRegisterNameCanNotBeDuplicatedWhenInserted(request.Email);

                Byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                User user = _mapper.Map<User>(request);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Status = true;

                User userRegister = await _userRepository.AddAsync(user);

                UserOperationClaim userOperationClaim = new() { UserId = userRegister.Id, OperationClaimId = 2 };
                await _userOperationClaimRepository.AddAsync(userOperationClaim);

                IPaginate<UserOperationClaim> userGetClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == userRegister.Id, include: i => i.Include(i => i.OperationClaim));

                AccessToken accessToken = _tokenHelper.CreateToken(user, userGetClaims.Items.Select(u => u.OperationClaim).ToList());
                return accessToken;
            }
        }
    }
}
