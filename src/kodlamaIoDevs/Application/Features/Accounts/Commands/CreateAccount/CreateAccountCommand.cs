using Application.Features.Accounts.Dtos;
using Application.Features.Accounts.Rules;
using Application.Services.AuthService;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand:IRequest<CreatedAccountDto>
    {
        public int UserId { get; set; }
        public string ProfileUrl { get; set; }

        public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreatedAccountDto>
        {

            private readonly IAccountRepository _accountRepository;
            private readonly IMapper _mapper;
            private readonly AccountBusinessRules _businessRules;

            public CreateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper, AccountBusinessRules businessRules)
            {
                _accountRepository = accountRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }


            public async Task<CreatedAccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.AccountNameCanNotBeDuplicatedWhenInserted(request.UserId);
                await _businessRules.UserShouldExistWhenRequested(request.UserId);
                Domain.Entities.Account mappedAccount = _mapper.Map<Domain.Entities.Account>(request);
                Domain.Entities.Account createdAccount = await _accountRepository.AddAsync(mappedAccount);
                CreatedAccountDto createdAccountDto = _mapper.Map<CreatedAccountDto>(createdAccount);
                return createdAccountDto;
            }
        }
    }
}
