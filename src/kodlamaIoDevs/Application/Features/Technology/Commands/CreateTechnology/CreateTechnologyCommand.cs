using Application.Features.Technology.Dtos;
using Application.Features.Technology.Rules;
using Application.Services.AuthService;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technology.Commands.CreateTechnology
{
    public class CreateTechnologyCommand:IRequest<CreatedTechnologyDto>
    {
        public string Name { get; set; }
        public int LanguageId { get; set; }

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {

            private readonly ITechnologyRepository _techologyRepository;
            private readonly IMapper _mapper;
            private TechnologyBusinessRules _technologyBusinessRules;

            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _techologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }


            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);
                await _technologyBusinessRules.LanguageShouldExistWhenRequested(request.LanguageId);
                Domain.Entities.Technology mappedTechnology = _mapper.Map<Domain.Entities.Technology>(request);
                Domain.Entities.Technology createdTechnology = await _techologyRepository.AddAsync(mappedTechnology);
                CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);
                return createdTechnologyDto;
            }
        }
    }
}
