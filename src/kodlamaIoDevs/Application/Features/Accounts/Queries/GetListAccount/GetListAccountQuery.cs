using Application.Features.Accounts.Models;
using Application.Services.AuthService;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Queries.GetListAccount
{
    public class GetListAccountQuery: IRequest<AccountListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListAccountQueryHandler : IRequestHandler<GetListAccountQuery, AccountListModel>
        {
            private readonly IAccountRepository _accountRepository;
            private readonly IMapper _mapper;

            public GetListAccountQueryHandler(IAccountRepository accountRepository,  IMapper mapper)
            {
                _accountRepository = accountRepository;
                _mapper = mapper;
            }

            public async Task<AccountListModel> Handle(GetListAccountQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Domain.Entities.Account> accounts = await _accountRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                AccountListModel mappedAccountListModel = _mapper.Map<AccountListModel>(accounts);
                return mappedAccountListModel;
            }
        }
    }
}
