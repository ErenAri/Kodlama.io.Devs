using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Rules
{
    public class AccountBusinessRules
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public AccountBusinessRules(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task AccountShouldExistWhenRequested(int id)
        {
            Domain.Entities.Account? account = await _accountRepository.GetAsync(g => g.Id == id);
            if (account == null) throw new BusinessException("Tech not exist! ");
        }

        public async Task UserShouldExistWhenRequested(int userUd)
        {
            User? user = await _userRepository.GetAsync(u => u.Id == userUd);
            if (user == null) throw new BusinessException("user not exist");
        }

        public async Task AccountNameCanNotBeDuplicatedWhenInserted(int userId)
        {
            IPaginate<Domain.Entities.Account> result = await _accountRepository.GetListAsync(g => g.UserId == userId);
            if (result.Items.Any()) throw new BusinessException("Github user id exists.");
        }
    }
}
