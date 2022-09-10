using Application.Features.Technology.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technology.Models
{
    public class TechnologyListModel
    {
        public IList<TechnologyListDto> Items { get; set; }
    }
}
