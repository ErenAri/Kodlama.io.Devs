using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technology.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly ILanguageRepository _languageRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository, ILanguageRepository languageRepository)
        {
            _technologyRepository = technologyRepository;
            _languageRepository = languageRepository;
        }

        public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Domain.Entities.Technology> result = await _technologyRepository.GetListAsync(t => t.Name == name);
            if (result.Items.Any()) throw new BusinessException("Tech name exist");
        }

        public async Task TechnologyShouldExistWhenRequested(int id)
        {
            Domain.Entities.Technology? language = await _technologyRepository.GetAsync(p => p.Id == id);
            if (language == null) throw new BusinessException("Requested technology not exist!");
        }

        public async Task LanguageShouldExistWhenRequested(int id)
        {
            Language? language = await _languageRepository.GetAsync(p => p.Id == id);
            if (language == null) throw new BusinessException("There is no language entered");
        }
        
        public async Task TechnologyNameCanNotBeDuplicatedWhenUpdated(string name)
        {
            IPaginate<Domain.Entities.Technology> result = await _technologyRepository.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology name exist");
        }
    }
}
