using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account:Entity
    {
        public int UserId { get; set; }
        public string ProfileUrl { get; set; }
        public virtual Language? Language { get; set; }

        public Account()
        {

        }

        public Account(int id, int userId,  string profileUrl):this()
        {
            Id = id;
            UserId = userId;
            ProfileUrl = profileUrl;
        }
    }
}
