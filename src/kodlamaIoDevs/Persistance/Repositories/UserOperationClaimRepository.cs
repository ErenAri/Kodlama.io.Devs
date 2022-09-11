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
    public class UserOperationClaimRepository: EfRepositoryBase<UserOperationClaim, BaseDbContexts>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(BaseDbContexts contexts): base(contexts)
        {

        }
    }
}
