using Application.Features.Accounts.Commands.CreateAccount;
using Application.Features.Accounts.Commands.UpdateAccount;
using Application.Features.Accounts.Dtos;
using Application.Features.Accounts.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Domain.Entities.Account, AccountListModel>().ReverseMap();
            CreateMap<IPaginate<Domain.Entities.Account>, AccountListModel>().ReverseMap();
            CreateMap<Domain.Entities.Account, AccountGetByIdDto>().ReverseMap();
            CreateMap<Domain.Entities.Account, AccountListDto>().ReverseMap();
            CreateMap<Domain.Entities.Account, CreateAccountCommand>().ReverseMap();
            CreateMap<Domain.Entities.Account, CreatedAccountDto>().ReverseMap();
            CreateMap<Domain.Entities.Account, UpdateAccountCommand>().ReverseMap();
            CreateMap<Domain.Entities.Account, UpdatedAccountDto>().ReverseMap();
            CreateMap<Domain.Entities.Account, DeletedAccountDto>().ReverseMap();
        }
    }
}
