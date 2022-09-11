using Application.Services.AuthService;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class LanguageRepository :EfRepositoryBase<Language, BaseDbContexts>, ILanguageRepository
    {
        public LanguageRepository(BaseDbContexts contexts): base(contexts)
        {

        }
    }
}
