using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.DTOs
{
    public class CreatedUserOperationClaimDto
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
