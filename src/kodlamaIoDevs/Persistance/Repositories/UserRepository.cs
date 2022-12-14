using Application.Services.AuthService;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class UserRepository:EfRepositoryBase<User, BaseDbContexts>, IUserRepository
    {
        public UserRepository(BaseDbContexts contexts): base(contexts)
        {

        }
    }
}
