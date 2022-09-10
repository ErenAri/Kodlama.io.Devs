using Application.Features.Accounts.Dtos;
using Application.Features.Accounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountCommand:IRequest<DeletedAccountDto>
    {
        public int Id { get; set; }

        public class DeleteAccountCommandHandler :IRequestHandler<DeleteAccountCommand, DeletedAccountDto>
        {
            private readonly IAccountRepository _accountRepository;
            private readonly IMapper _mapper;
            private readonly AccountBusinessRules _businessRules;

            public async Task<DeletedAccountDto> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.AccountShouldExistWhenRequested(request.Id);
                Domain.Entities.Account? account = await _accountRepository.GetAsync(p => p.Id == request.Id);
                Domain.Entities.Account accountLanguage = await _accountRepository.DeleteAsync(account);
                DeletedAccountDto deletedAccountDto = _mapper.Map<DeletedAccountDto>(accountLanguage);
                return deletedAccountDto;
            }
        }
    }
}
