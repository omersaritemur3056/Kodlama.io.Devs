using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            this.operationClaimRepository = operationClaimRepository;
        }

        public async Task OperationClaimCanNotBeDuplicated(string name)
        {
            OperationClaim? result = await operationClaimRepository.GetAsync(o => o.Name == name);
            if (result != null) throw new BusinessException("Operation claim can't be duplicated");
        }

        public Task OperationClaimMustBeExistWhenRequested(OperationClaim operationClaim)
        {
            if (operationClaim == null) throw new BusinessException("Operation claim does't exist");
            return operationClaimRepository.GetAsync(o => o.Name == operationClaim.Name);
        }
    }
}
