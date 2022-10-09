using Application.Features.OperationClaims.DTOs;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Models
{
    public class GetListOperationClaimModel : BasePageableModel
    {
        public IList<GetListOperationClaimDto> Items { get; set; }
    }
}
