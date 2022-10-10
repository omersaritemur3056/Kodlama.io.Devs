using Application.Features.OperationClaims.DTOs;
using Application.Features.UserOperationClaims.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Models
{
    public class GetListUserOperationClaimModel
    {
        public List<GetListUserOperationClaimDto> Items { get; set; }
    }
}
