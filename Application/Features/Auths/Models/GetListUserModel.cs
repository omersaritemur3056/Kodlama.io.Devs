using Application.Features.Auths.DTOs;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Models
{
    public class GetListUserModel : BasePageableModel
    {
        public IList<GetListUserDto> Items { get; set; }
    }
}
