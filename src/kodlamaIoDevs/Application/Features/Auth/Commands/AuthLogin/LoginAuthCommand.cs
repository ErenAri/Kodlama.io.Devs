using Application.Services.AuthService;
using Core.CrossCuttingConcerns.Exceptions;
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

namespace Application.Features.Auth.Commands.AuthLogin
{
    public class LoginAuthCommand: IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class AuthLoginCommandHandler : IRequestHandler<LoginAuthCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;

            public AuthLoginCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules, IUserOperationClaimRepository userOperationClaimRepository)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _authBusinessRules = authBusinessRules;
                _userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<AccessToken> Handle(LoginAuthCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.AuthLoginEmailCheck(request.Email);
                User user = await _userRepository.GetAsync(u => u.Email == request.Email);
                if(!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)) throw new BusinessException("Password incorrect!");

                IPaginate<UserOperationClaim> userGetClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id, include: i => i.Include(i => i.OperationClaim));
                AccessToken accessToken = _tokenHelper.CreateToken(user, userGetClaims.Items.Select(u => u.OperationClaim).ToList());
                return accessToken;
            }
        }
    }
}
