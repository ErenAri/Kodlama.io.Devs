using Application.Features.Technology.Dtos;
using Application.Features.Technology.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technology.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand :IRequest<UpdatedTechnologyDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }

        public class UpdateTechnologyCommandHandler:IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private TechnologyBusinessRules _technologyBusinessRules;

            public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }


            public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechnologyShouldExistWhenRequested(request.Id);
                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);
                await _technologyBusinessRules.LanguageShouldExistWhenRequested(request.LanguageId);
                Domain.Entities.Technology? technology = _mapper.Map<Domain.Entities.Technology>(request);
                Domain.Entities.Technology updateTechnology = await _technologyRepository.UpdateAsync(technology);
                UpdatedTechnologyDto updatedTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updateTechnology);
                return updatedTechnologyDto;

            }
        }
    }
}
