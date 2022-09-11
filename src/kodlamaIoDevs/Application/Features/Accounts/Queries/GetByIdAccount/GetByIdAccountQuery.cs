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

namespace Application.Features.Accounts.Queries.GetByIdAccount
{
    public class GetByIdAccountQuery : IRequest<AccountGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdAccountQueryHandler : IRequestHandler<GetByIdAccountQuery, AccountGetByIdDto>
        {
            private readonly IAccountRepository _accountRepository;
            private readonly IMapper _mapper;
            private readonly AccountBusinessRules _businessRules;


            public GetByIdAccountQueryHandler(IAccountRepository accountRepository, IMapper mapper, AccountBusinessRules businessRules)
            {
                _accountRepository = accountRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<AccountGetByIdDto> Handle(GetByIdAccountQuery request, CancellationToken cancellationToken)
            {
                await _businessRules.AccountShouldExistWhenRequested(request.Id);
                Domain.Entities.Account account = await _accountRepository.GetAsync(g => g.Id == request.Id);
                AccountGetByIdDto mappedAccountDto = _mapper.Map<AccountGetByIdDto>(account);
                return mappedAccountDto;
            }
        }
    }
}
