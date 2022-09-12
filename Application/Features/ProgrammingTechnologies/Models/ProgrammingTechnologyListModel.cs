using Application.Features.ProgrammingTechnologies.DTOs;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingTechnologies.Models
{
    public class ProgrammingTechnologyListModel : BasePageableModel
    {
        public IList<ProgrammingTechnologyListDto> Items { get; set; }
    }
}
