﻿using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.Auth.Commands.AuthLogin
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;
        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task AuthRegisterNameCanNotBeDuplicatedWhenInserted(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(u => u.Email == email);
            if (result.Items.Any()) throw new BusinessException("There is such an email in the system");
        }
        public async Task AuthLoginEmailCheck(string email)
        {
            User result = await _userRepository.GetAsync(u => u.Email == email);
            if (result == null) throw new BusinessException("Such an email was not found in system");
        }

    }
}