using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository
                .GetListAsync(pl => pl.Name == name);

            if (result.Items.Any()) throw new BusinessException("This programming language name exist");
        }

        public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage programmingLanguage)
        {
            if (programmingLanguage == null) throw new BusinessException("Requested Language does not exist");
        }

        public async Task ProgrammingLanguageMustBeExistForDelete(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository
                .GetListAsync(pl => pl.Name == name);

            if (!result.Items.Any()) throw new BusinessException("Deleting programming language does not exist");

        }

        public async Task UpdatingIdOfProgrammingLanguageMustBeExist(int id)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository
                .GetListAsync(pl => pl.Id == id);

            if (!result.Items.Any()) throw new BusinessException("İd not found");
        }
    }
}
