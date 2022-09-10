﻿using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Technology:Entity
    {
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public virtual Language? Language { get; set; }

        public Technology()
        {

        }

        public Technology(int id, string name, int languageId):this()
        {
            Id = id;
            Name = name;
            LanguageId = languageId;
        }
    }
}
