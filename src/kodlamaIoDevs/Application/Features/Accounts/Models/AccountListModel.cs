using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Models
{
    public class AccountListModel:BasePageableModel
    {
        public IList<AccountListModel> Items { get; set; }
    }
}
