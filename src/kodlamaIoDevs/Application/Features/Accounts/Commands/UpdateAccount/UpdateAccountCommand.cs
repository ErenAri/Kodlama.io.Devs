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

namespace Application.Features.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommand:IRequest<UpdatedAccountDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProfileUrl { get; set; }

        public class UpdateAccountCommandHandler: IRequestHandler<UpdateAccountCommand, UpdatedAccountDto>
        {
            private readonly IAccountRepository _accountRepository;
            private readonly IMapper _mapper;
            private readonly AccountBusinessRules _businessRules;


            public UpdateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper, AccountBusinessRules businessRules)
            {
                _accountRepository = accountRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<UpdatedAccountDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.AccountShouldExistWhenRequested(request.Id);
                await _businessRules.UserShouldExistWhenRequested(request.UserId);
                Domain.Entities.Account? account = _mapper.Map<Domain.Entities.Account>(request);
                Domain.Entities.Account updateAccount = await _accountRepository.UpdateAsync(account);
                UpdatedAccountDto updatedAccountDto = _mapper.Map<UpdatedAccountDto>(updateAccount);
                return updatedAccountDto;
            }

            
        }
    }
}
