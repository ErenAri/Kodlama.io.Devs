﻿using Application.Services.AuthService;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Rules
{
    public class LanguageBusinessRules
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageBusinessRules(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task LanguageNameCanNotBeDuplicatedWhenInsertedOrUpdated(string name)
        {
            IPaginate <Language> result = await _languageRepository.GetListAsync(x => x.Name==name);
            if (result.Items.Any()) throw new BusinessException("Language name exist!");
        }

        public void LanguageShouldExistWhenRequested(Language? language)
        {
            if (language == null) throw new BusinessException("Requested language does not exist");
        }

        public async Task LanguageShouldExistWhenDeleted(int id)
        {
            const int V = 1;
            Language? result = await _languageRepository.GetAsync(V => V.Id == id);
            if (result == null) throw new BusinessException("To be deleted programming language does not exist!");
        }
    }
}
